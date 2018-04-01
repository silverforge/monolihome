using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoNoLiHome.Model.Configuration;

namespace MoNoLiHome.Network.Client
{
    public class FirebaseConnector : IFirebaseConnector
    {
        readonly MoNoLiHomeConfig _moNoLiHomeConfig;
        readonly Fetch _fetch;

        public FirebaseConnector(IOptions<MoNoLiHomeConfig> monoliHomeConfig)
        {
            _moNoLiHomeConfig = monoliHomeConfig.Value;
            var fetchConfig = new FetchConfig()
            {
                Host = _moNoLiHomeConfig.BaseUrl,
                Path = string.Format($"{_moNoLiHomeConfig.EndpointPath}/{_moNoLiHomeConfig.Function}")
            };
            _fetch = new Fetch(fetchConfig);
        }

        public async Task<bool> PingMotionDetectedAsync()
        {
            return await _fetch.GetAsync<bool>();
        }
    }
}
