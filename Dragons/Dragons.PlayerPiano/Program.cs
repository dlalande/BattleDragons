using System;
using System.Linq;
using Dragons.Core;
using Dragons.Service.Core;
using System.Threading.Tasks;
using Dragons.Core.Models;
using Dragons.Core.Types;

namespace Dragons.PlayerPiano
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new DragonsClient(new Uri("http://localhost:51962/"), string.Empty);
            
            //foreach (var file in Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "GamePlays"), "*.json"))
            //{
            //    var gamePlay = JsonConvert.DeserializeObject<GamePlay>(File.ReadAllText(file));
            //    PlayGameAsync(gamePlay).Wait();
            //}

            PlayGameAutoAsync(GetRandomGamePlay()).Wait();
            Console.ReadLine();
        }

        public static async Task PlayGameAutoAsync(GamePlay gamePlay)
        {
            try
            {

                var gameService = new GameService();
                await gameService.InitializeAsync(@"C:\Users\David\Source\Repos\BattleDragons\Dragons\Dragons.Service\App_Data\InitialSetups");
                await gameService.InsertGameStartAsync(gamePlay.GameStart);
                var offset = 0;
                var i = 0;
                while (true)
                {
                    var move = gameService.GetRandomMove(14, int.MaxValue);
                    if (i % 2 == 0)
                        move.Player = gamePlay.GameStart.Player1;
                    else
                        move.Player = gamePlay.GameStart.Player2;
                    i++;
                    await gameService.InsertGameMoveAsync(move);
                    var events = await gameService.GetGameEventsAsync(gamePlay.GameStart.Player1.PlayerId, offset);
                    foreach (var gameEvent in events)
                    {
                        if (gameEvent.Type == EventType.DragonKilled)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        if (gameEvent.Type == EventType.GameWon)
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(gameEvent.ToString());
                        Console.ResetColor();
                        offset++;
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static async Task PlayGameAsync(GamePlay gamePlay)
        {
            try
            {
                var gameService = new GameService();
                await gameService.InitializeAsync(@"C:\Users\David\Source\Repos\BattleDragons\Dragons\Dragons.Service\App_Data\InitialSetups");
                await gameService.InsertGameStartAsync(gamePlay.GameStart);
                var offset = 0;
                foreach (var move in gamePlay.Moves)
                {
                    await gameService.InsertGameMoveAsync(move);
                    var events = await gameService.GetGameEventsAsync(gamePlay.GameStart.Player1.PlayerId, offset);
                    foreach (var gameEvent in events)
                    {
                        Console.WriteLine(gameEvent.ToString());
                        offset++;
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static GamePlay GetRandomGamePlay()
        {
            var gameService = new GameService();
            gameService.InitializeAsync(@"C:\Users\David\Source\Repos\BattleDragons\Dragons\Dragons.Service\App_Data\InitialSetups").Wait();

            var players = gameService.GetRandomPlayerPair();

            var moves = Enumerable.Range(0, 200).Select(i =>
            {
                var move = gameService.GetRandomMove(14, int.MaxValue);
                if (i % 2 == 0)
                    move.Player = players.Item1;
                else
                    move.Player = players.Item2;
                return move;
            }).ToList();

            return new GamePlay()
            {
                GameStart = new GameStart
                {
                    Player1 = players.Item1,
                    Player2 = players.Item2
                },
                Moves = moves
            };
        }
    }
}
