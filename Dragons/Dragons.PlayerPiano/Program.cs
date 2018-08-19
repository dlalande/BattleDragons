using System;
using System.Linq;
using System.Collections.Generic;
using Dragons.Core;
using Dragons.Service.Core;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

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
                    var move = gameService.GetRandomMove(14);
                    if (i % 2 == 0)
                        move.PlayerId = gamePlay.GameStart.Player1.PlayerId;
                    else
                        move.PlayerId = gamePlay.GameStart.Player2.PlayerId;
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

            var moves = Enumerable.Range(0, 20).Select(i =>
            {
                var move = gameService.GetRandomMove(14);
                if (i % 2 == 0)
                    move.PlayerId = players.Item1.PlayerId;
                else
                    move.PlayerId = players.Item2.PlayerId;
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
