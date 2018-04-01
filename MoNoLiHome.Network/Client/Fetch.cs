using RestSharp;
using System.Threading.Tasks;
using System;

namespace MoNoLiHome.Network.Client
{
    public class Fetch
    {
        readonly FetchConfig _fetchConfig;
        readonly RestClient _client;

        public Fetch(FetchConfig fetchConfig)
        {
            _fetchConfig = fetchConfig;
            _client = new RestClient(fetchConfig.Host);
        }

        public async Task<T> GetAsync<T>()
        {
            var request = new RestRequest(_fetchConfig.Path, Method.GET);
            request.AddHeader("Content-Type", "application/json; charset=utf8");

            var result = await _client.ExecuteTaskAsync(request);
            var content = result.Content;
            return (T)Convert.ChangeType(result.IsSuccessful, typeof(T));
        }
    }
}
