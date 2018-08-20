using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dragons.Client;
using Dragons.Core;

namespace Dragons.Console
{
    class Program
    {
        static DragonsClient _client;

        enum PlayerMode
        { 
            Waiter, 
            Joiner
        }
        
        static void Main(string[] args)
        {
            _client = new DragonsClient(new Uri("http://localhost:51962/"), string.Empty);
            var playerTasks = new [] { StartGameAsync(PlayerMode.Waiter), StartGameAsync(PlayerMode.Joiner) };
            Task.WaitAll(playerTasks);
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
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    } while (reservations == null || reservations.Count < 1);

                    await _client.InsertGameStartAsync(new GameStart {Player1 = reservations[0].Player, Player2 = player});
                }

                await PlayGameAsync(player);
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine(e);
            }
        }

        static async Task PlayGameAsync(Player player)
        {
            try
            {
                var eventOffset = 0;
                while (true)
                {
                    Game game;
                    do
                    {
                        game = await _client.GetGameAsync(player.PlayerId);
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    } while (game == null);

                    if (game.CanMove && !game.IsOver)
                    {
                        var move = await _client.GetRandomMoveAsync(14);
                        move.Player = player;
                        //move.Spell = Spell.AllSpells.Single(spell => spell.Type == SpellType.AvadaKedavra);
                        await _client.InsertGameMoveAsync(move);
                        System.Console.WriteLine($"{Environment.NewLine}{player.Name}: {move}");
                    }
                    else
                    {
                        var hasWon = false;
                        var events = await _client.GetGameEventsAsync(player.PlayerId, eventOffset);
                        foreach (var gameEvent in events)
                        {
                            switch (gameEvent.Type)
                            {
                                case EventType.GameStarted:
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case EventType.DragonKilled:
                                    System.Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case EventType.GameWon:
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    hasWon = true;
                                    break;
                                case EventType.Attacked:
                                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case EventType.ManaUpdated:
                                    System.Console.ForegroundColor = gameEvent.Mana > 0 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;
                                    break;
                            }

                            System.Console.WriteLine($"{Environment.NewLine}{player.Name}: {gameEvent}");
                            System.Console.ResetColor();
                            eventOffset++;
                        }

                        if (hasWon)
                            break;
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}
