using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dragons.Client;
using Dragons.Core;
using System.Text;
using System.IO;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.Console
{
    class Program
    {
        enum PlayerMode
        { 
            Waiter, 
            Joiner
        }
        
        const string LogFilePath = "Log.txt";
        static DragonsClient _client;
        static readonly ConcurrentQueue<Tuple<string, ConsoleColor>> _waiterLog = new ConcurrentQueue<Tuple<string, ConsoleColor>>();
        static readonly ConcurrentQueue<Tuple<string, ConsoleColor>> _joinerLog = new ConcurrentQueue<Tuple<string, ConsoleColor>>();

        static void Main(string[] args)
        {
            _client = new DragonsClient(new Uri("http://localhost:51962/"), Guid.NewGuid().ToString(), Constants.ValidApiKey);

            if (args.Length == 0)
            {
                SinglePlayerStart(PlayerType.HardComputer, PlayerType.HardComputer).Wait();
            }
            else
            {
                var playerTasks = new[] 
                {
                    MultiPlayerStart(PlayerType.HardComputer, PlayerMode.Waiter),
                    MultiPlayerStart(PlayerType.HardComputer, PlayerMode.Joiner)};
                Task.WaitAll(playerTasks);
            }

            System.Console.WriteLine("Done!");

            var joinerLogString = new StringBuilder();
            var waiterLogString = new StringBuilder();
            while (_waiterLog.TryDequeue(out var log))
            {
                waiterLogString.AppendLine(log.Item1);
                System.Console.ForegroundColor = log.Item2;
                System.Console.WriteLine(log.Item1);
            }
           
            while (_joinerLog.TryDequeue(out var log))
            {
                joinerLogString.AppendLine(log.Item1);
                System.Console.ForegroundColor = log.Item2;
                System.Console.WriteLine(log.Item1);
            }
            File.WriteAllText(LogFilePath,$"{waiterLogString}{Environment.NewLine}{joinerLogString}");
            System.Console.ReadLine();
        }

        static async Task SinglePlayerStart(PlayerType player1Type, PlayerType player2Type)
        {
            try
            {
                var players = await _client.GetRandomPlayerPairAsync();
                var player1 = players.Item1;
                var player2 = players.Item2;

                player1.Type = player1Type;
                player2.Type = player2Type;

                await _client.InsertReservationAsync(new Reservation { Player = player1 });
                await _client.InsertGameStartAsync(new GameStart { Player1 = player1, Player2 = player2 });
                System.Console.Write($"Playing game {player1.Name} vs. {player2.Name} ...");
                await PlayGameAsync(player1, PlayerMode.Waiter);
            }
            catch (Exception e)
            {
                LogEvent(PlayerMode.Waiter, e.Message, ConsoleColor.DarkRed);
                System.Console.WriteLine(e.Message);
            }
        }

        static async Task MultiPlayerStart(PlayerType playerType, PlayerMode playerMode)
        {
            try
            {
                var player = await _client.GetRandomPlayerAsync();
                player.Type = playerType;
                if (playerMode == PlayerMode.Waiter)
                    await _client.InsertReservationAsync(new Reservation {Player = player});
                else
                {
                    List<Reservation> reservations;
                    do
                    {
                        reservations = await _client.GetReservationsAsync();
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                    } while (reservations == null || reservations.Count < 1);

                    await _client.InsertGameStartAsync(new GameStart {Player1 = reservations[0].Player, Player2 = player});
                    System.Console.Write($"Playing game {reservations[0].Player.Name} vs. {player.Name} ...");
                }

                await PlayGameAsync(player, playerMode);
            }
            catch (Exception e)
            {
                LogEvent(playerMode, e.Message, ConsoleColor.DarkRed);
                System.Console.WriteLine(e.Message);
            }
        }

        static async Task PlayGameAsync(Player player, PlayerMode playerMode)
        {
            try
            {
                var eventOffset = 0;
                var printedStart = false;
                while (true)
                {
                    Game game;
                    do
                    {
                        game = await _client.GetGameAsync(player.PlayerId);
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                    } while (game == null);

                    if (game.CanMove && !game.IsOver)
                    {
                        if (!printedStart)
                        {
                            LogEvent(playerMode, $"{game}", ConsoleColor.Magenta);
                            printedStart = true;
                        }

                        Move move;
                        if (player.Type == PlayerType.Human)
                        {
                            System.Console.WriteLine();
                            System.Console.Write($"Enter attack coordinates X:");
                            int.TryParse(System.Console.ReadLine(), out var x);
                            System.Console.Write($"Enter attack coordinates Y:");
                            int.TryParse(System.Console.ReadLine(), out var y);
                            System.Console.Write($"Enter attack :");
                            int.TryParse(System.Console.ReadLine(), out var spellIndex);

                            move = new Move()
                            {
                                Player = player,
                                Coordinate = new Coordinate()
                                {
                                    X = x,
                                    Y = y
                                }, 
                                Spell = Constants.AllSpells[spellIndex]
                            };
                        }
                        else
                            move = await _client.GetNextMoveAsync(player.PlayerId);

                        //move.Spell = Spell.AllSpells.Single(spell => spell.Type == SpellType.AvadaKedavra);
                        await _client.InsertGameMoveAsync(move);
                        LogEvent(playerMode, $"{Environment.NewLine}{player.Name}: {move}", ConsoleColor.White);
                    }

                    if (!printedStart)
                    {
                        LogEvent(playerMode, $"{game}", ConsoleColor.Magenta);
                        printedStart = true;
                    }
                    var hasWon = false;
                    var events = await _client.GetGameEventsAsync(player.PlayerId, eventOffset);
                    foreach (var gameEvent in events)
                    {
                        var color = ConsoleColor.White; 
                        switch (gameEvent.Type)
                        {
                            case EventType.GameStarted:
                                color = ConsoleColor.Magenta;
                                break;
                            case EventType.DragonKilled:
                                color = ConsoleColor.Green;
                                break;
                            case EventType.GameWon:
                                color = ConsoleColor.Magenta;
                                hasWon = true;
                                break;
                            case EventType.Attacked:
                                color = ConsoleColor.Yellow;
                                break;
                            case EventType.ManaUpdated:
                                color = gameEvent.Mana > 0 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;
                                break;
                        }
                        if(gameEvent.Type != EventType.GameStarted)
                            LogEvent(playerMode, $"{Environment.NewLine}{player.Name}: {gameEvent}", color);
                        eventOffset++;
                    }

                    if (hasWon)
                        break;
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                }
            }
            catch (Exception e)
            {
                LogEvent(playerMode, e.Message, ConsoleColor.DarkRed);
                throw;
            }
        }
        static void LogEvent(PlayerMode mode, string log, ConsoleColor color)
        {
            var logger = mode == PlayerMode.Waiter ? _waiterLog : _joinerLog;
            logger.Enqueue(new Tuple<string, ConsoleColor>(log, color));
            System.Console.Write(".");

            //if (mode == PlayerMode.Waiter)
            //{
            //    System.Console.ForegroundColor = color;
            //    System.Console.WriteLine(log);
            //}
        }
    }
}
