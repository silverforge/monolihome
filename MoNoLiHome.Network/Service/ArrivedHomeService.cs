using System;
using System.Threading.Tasks;
using MoNoLiHome.Network.Client;

namespace MoNoLiHome.Network.Service
{
    public class ArrivedHomeService
    {
        const string IAMHOMEKEY = "I_AM_HOME";

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
    }
}
