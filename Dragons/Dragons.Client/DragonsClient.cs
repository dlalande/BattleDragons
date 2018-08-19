using Dragons.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Client
{
    public class DragonsClient : IDisposable
    {
        private const string JsonMediaType = "application/json";
        private const string GameRoute = "game";
        private const string ReservationRoute = "reservation";
        private const string GameStartRoute = "gamestart";
        private const string EventRoute = "event";
        private const string MoveRoute = "move";

        private const string RoutePrefix = "dragons";
        private readonly Uri baseAddress;
        private string apiKey;

        public DragonsClient(Uri baseAddress, string apiKey)
        {
            this.baseAddress = baseAddress;
            this.apiKey = apiKey;
        }

        public async Task<Game> GetGameAsync(string playerId)
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{playerId}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Game>(content);
            }
        }

        public async Task<List<Event>> GetGameEventsAsync(string playerId, int offset = 0)
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.GetAsync($"{RoutePrefix}/{GameRoute}/{playerId}/{EventRoute}/?offset={offset}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Event>>(content);
            }
        }

        public async Task<Move> InsertGameMoveAsync(Move move)
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.PostAsync($"{RoutePrefix}/{GameRoute}/{MoveRoute}", new StringContent(JsonConvert.SerializeObject(move), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Move>(content);
            }
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.GetAsync($"{RoutePrefix}/{ReservationRoute}");
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Reservation>>(content);
            }
        }

        public async Task<Reservation> InsertReservationAsync(Reservation reservation)
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.PostAsync($"{RoutePrefix}/{ReservationRoute}", new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Reservation>(content);
            }
        }

        public async Task DeleteReservationAsync(Reservation reservation)
        {
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
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
            using (var client = new HttpClient() { BaseAddress = this.baseAddress })
            {
                var response = await client.PostAsync($"{RoutePrefix}/{GameStartRoute}", new StringContent(JsonConvert.SerializeObject(gameStart), Encoding.UTF8, JsonMediaType));
                response.EnsureSuccess();
            }
        }

        public Task InitializeAsync(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
