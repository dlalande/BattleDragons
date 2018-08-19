using System;
using System.Threading.Tasks;
using Dragons.Client;

namespace Dragons.Console
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var playerTasks = new Task[] { PlayGameAsync() };
            Task.WaitAll(playerTasks);
            System.Console.ReadLine();
        }

        static async Task PlayGameAsync()
        {
            var playerId = Guid.NewGuid().ToString();
            var currentEventIndex = 0;
            
            using (var dragonsClient = new DragonsClient(new Uri("http://localhost:51962/"), string.Empty))
            {
                var game = await dragonsClient.GetGameAsync(playerId);
            }
        }
    }
}
