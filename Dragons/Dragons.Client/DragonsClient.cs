using Dragons.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dragons.Core.Models;

namespace Dragons.Client
{
    public class DragonsClient 
    {
        private const string JsonMediaType = "application/json";
        private const string GameRoute = "game";
        private const string ReservationRoute = "reservation";
        private const string GameStartRoute = "gamestart";
        private const string EventRoute = "events";
        private const string MoveRoute = "move";
        private const string RandomRoute = "random";
        private const string PlayerRoute = "player";

        private const string RoutePrefix = "dragons";
        private readonly Uri baseAddress;
        private readonly string apiKey;
        private readonly string clientId;

        public DragonsClient(Uri baseAddress, string clientId, string apiKey)
        {
            this.baseAddress = baseAddress;
            this.clientId = clientId;
            this.apiKey = apiKey;
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient() {BaseAddress = this.baseAddress};
            client.DefaultRequestHeaders.Add(Constants.ClientIdHeader, clientId);
            client.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, apiKey);
            return client;
        }

        public async Task<Player> GetRandomPlayerAsync()
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{PlayerRoute}/{RandomRoute}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Player>(content);
            }
        }

        public async Task<Move> GetRandomMoveAsync(int boardSize)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{MoveRoute}/{RandomRoute}/{boardSize}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Move>(content);
            }
        }

        public async Task<Move> GetNextMoveAsync(string playerId)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{MoveRoute}/{playerId}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Move>(content);
            }
        }

        public async Task<Game> GetGameAsync(string playerId)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{playerId}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Game>(content);
            }
        }

        public async Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{playerId}/{EventRoute}/?offset={offset}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Event>>(content);
            }
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.PostAsync($"{RoutePrefix}/{GameRoute}/{MoveRoute}", new StringContent(JsonConvert.SerializeObject(move), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Move>(content);
            }
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync($"{RoutePrefix}/{ReservationRoute}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Reservation>>(content);
            }
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.PostAsync($"{RoutePrefix}/{ReservationRoute}", new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Reservation>(content);
            }
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            using (var client = GetHttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{RoutePrefix}/{ReservationRoute}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, JsonMediaType)
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccess();
            }
        }

        public async Task InsertGameStartAsync(GameStart gameStart)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.PutAsync($"{RoutePrefix}/{GameStartRoute}", new StringContent(JsonConvert.SerializeObject(gameStart), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
            }
        }
    }
}
