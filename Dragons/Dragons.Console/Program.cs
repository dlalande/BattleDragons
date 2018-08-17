using System;
using System.Threading;
using System.Threading.Tasks;
using Dragons.Core;
using Dragons.Service.Core;
using Newtonsoft.Json;

namespace Dragons.Console
{
    class Program
    {
        private static readonly IGameService Service = new GameService();
        private static string PlayerId = Guid.NewGuid().ToString();
        private static int _currentEventIndex = 0;


        static void Main(string[] args)
        {
            MainAsync(args).Wait();
            System.Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            var res = JsonConvert.SerializeObject(new Reservation { Player = {PlayerId = PlayerId, Name = "David" }});
            var move = JsonConvert.SerializeObject(new Move
            {
                Coordinate = new Coordinate
                {
                    X = 4,
                    Y = 5
                },
                PlayerId = PlayerId,
                Spell = new Spell
                {
                    Type = SpellType.FireBall,
                    Description = "description",
                    ManaCost = 35
                }
            });
            if (args.Length > 0) // Player 2
            {
                var reservations = await Service.GetReservationsAsync();
                var gameStart = new GameStart()
                {
                    Player1 = reservations[0].Player,
                    Player2 = { PlayerId = PlayerId, Name = "Taras" }
                };
                var g = JsonConvert.SerializeObject(gameStart);
                await Service.InsertGameStartAsync(gameStart);
            }
            else // Player 1
            {
                await Service.InsertReservationAsync(new Reservation() {Player = {PlayerId = PlayerId, Name = "David"}});
            }

            await PlayGame();

        }

        static async Task PlayGame()
        {
            PlayerId = "f1b8b107-2f0c-4de6-8582-4e81d9d3e563";
            Game game;
            do
            {
                game = await Service.GetGameAsync(PlayerId);
                if (game == null)
                    Thread.Sleep(1000);
            } while (game == null);
            
            System.Console.WriteLine("Game started...");
            System.Console.WriteLine(game.ToString());

            var events = await Service.GetGameEventsAsync(PlayerId, _currentEventIndex);

            foreach (var gameEvent in events)
            {
                _currentEventIndex++;
                System.Console.WriteLine($"{gameEvent}.");
            }
            await Service.InsertGameMoveAsync(new Move()
            {
                Coordinate = new Coordinate {X = 4, Y = 7},
                PlayerId = PlayerId,
                Spell = game.Spells[1]
            });
        }
    }
}
