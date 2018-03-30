using System;
using System.Threading.Tasks;
using MoNoLiHome.Network.Client;

namespace MoNoLiHome.Network.Service
{
    public class ArrivedHomeService : IArrivedHomeService
    {
        const string IAMHOMEKEY = "I_AM_HOME";
        const int TTL = 3;

        readonly IRedisConnector _redisConnector;

        public ArrivedHomeService(IRedisConnector redisConnector)
        {
            _redisConnector = redisConnector;
        }

        public async Task<bool> AmIHomeAsync()
        {
            var retValue = false;
            var value = await _redisConnector.GetAsync(IAMHOMEKEY);
            if (!String.IsNullOrEmpty(value))
                retValue = Boolean.Parse(value);

            return retValue;
        }

        public async Task<bool> IAmHomeAsync(bool toggle)
        {
            var result = await _redisConnector.SetAsync(IAMHOMEKEY, toggle.ToString(), TimeSpan.FromHours(TTL));
            return toggle;
        }
    }
}
