using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace SevenWestMedia.Console.Demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _client;
        private string _token;
        private const string ApiKeyHeader = "apikey";
        private const string AuthTokenHeader = "auth-token";

        public UserRepository(HttpClient client)
        {
            _client = client;
        }

        public string Authenticate(string apiKey)
        {
            if (!_client.DefaultRequestHeaders.Contains(ApiKeyHeader))
            {
                _client.DefaultRequestHeaders.Add(ApiKeyHeader, apiKey);
            }
            
            var responseTask = _client.GetAsync("auth");
            responseTask.Wait();

            var result = responseTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var readTask = result.Content.ReadAsStringAsync();
            readTask.Wait();

            _token = readTask.Result;
            return _token;
        }

        public string GetUserNameById(int id)
        {
            if (!_client.DefaultRequestHeaders.Contains(AuthTokenHeader))
            {
                _client.DefaultRequestHeaders.Add(AuthTokenHeader, _token);
            }

            var responseTask = _client.GetAsync($"user/name/{id}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var readTask = result.Content.ReadAsStringAsync();
            readTask.Wait();

            return readTask.Result;
        }

        public string GetUsersByAge(int age)
        {
            if (!_client.DefaultRequestHeaders.Contains(AuthTokenHeader))
            {
                _client.DefaultRequestHeaders.Add(AuthTokenHeader, _token);
            }

            var responseTask = _client.GetAsync($"user/age/{age}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var readTask = result.Content.ReadAsStringAsync();
            readTask.Wait();

            return readTask.Result;
        }

        public List<string> GetByGendersPerAge()
        {
            if (!_client.DefaultRequestHeaders.Contains(AuthTokenHeader))
            {
                _client.DefaultRequestHeaders.Add(AuthTokenHeader, _token);
            }

            var responseTask = _client.GetAsync("user/genders/age");
            responseTask.Wait();

            var result = responseTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var readTask = result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<string>>(readTask);
        }
    }
}
