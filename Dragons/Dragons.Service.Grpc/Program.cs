using Dragons.Core.Grpc;
using Grpc.Core;
using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace Dragons.Service.Grpc
{
    class Program
    {
        private const int PortStartIndex = 8000;
        private const int PortEndIndex = 9000;

        static void Main(string[] args)
        {//var port = GetOpenPort();
            var port = 50051;

            var gameService = new Core.GameService();
            gameService.InitializeAsync(Path.Combine(Environment.CurrentDirectory, "InitialSetups")).ConfigureAwait(false).GetAwaiter().GetResult();

            Server server = new Server
            {
                Services = { GameService.BindService(new GameServiceImpl(gameService)) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("GameService server listening on port " + port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
        private static int GetOpenPort()
        {
            var usedPorts = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(p => p.Port).ToList();
            for (var port = PortStartIndex; port < PortEndIndex; port++)
                if (!usedPorts.Contains(port))
                    return port;
            throw new Exception($"Unable to find open port in range ({PortStartIndex},{PortEndIndex})");
        }
    }
}
