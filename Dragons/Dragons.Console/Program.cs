using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dragons.Client;
using Dragons.Core;
using System.Text;
using System.IO;

namespace Dragons.Console
{
    class Program
    {
        const string LogFilePath = "Log.txt";

        static DragonsClient _client;
        static ConcurrentQueue<Tuple<string, ConsoleColor>> _waiterLog = new ConcurrentQueue<Tuple<string, ConsoleColor>>();
        static ConcurrentQueue<Tuple<string, ConsoleColor>> _joinerLog = new ConcurrentQueue<Tuple<string, ConsoleColor>>();

        enum PlayerMode
        { 
            Waiter, 
            Joiner
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
        
        static void Main(string[] args)
        {
            _client = new DragonsClient(new Uri("http://localhost:51962/"), Constants.ApiKey);
            var playerTasks = new [] { StartGameAsync(PlayerMode.Waiter), StartGameAsync(PlayerMode.Joiner) };
            Task.WaitAll(playerTasks);
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

        static async Task StartGameAsync(PlayerMode playerMode)
        {
            try
            {
                var player = await _client.GetRandomPlayerAsync();
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
                LogEvent(playerMode, e.ToString(), ConsoleColor.DarkRed);
                System.Console.WriteLine(e);
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
                        var move = await _client.GetRandomMoveAsync(14);
                        move.Player = player;
                        //move.Spell = Spell.AllSpells.Single(spell => spell.Type == SpellType.AvadaKedavra);
                        await _client.InsertGameMoveAsync(move);
                        LogEvent(playerMode, $"{Environment.NewLine}{player.Name}: {move}", ConsoleColor.White);
                    }
                    else
                    {
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
            }
            catch (Exception e)
            {
                LogEvent(playerMode, e.ToString(), ConsoleColor.DarkRed);
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}
