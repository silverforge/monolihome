using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoNoLiHome.Network.Client;

namespace MoNoLiHome.Network.Service
{
    public class ArrivedHomeService : IArrivedHomeService
    {
        const string IAMHOMEKEY = "I_AM_HOME";
        const int TTL = 3;

        readonly IRedisConnector _redisConnector;
        readonly ILogger<IArrivedHomeService> _logger;

        public ArrivedHomeService(IRedisConnector redisConnector, ILogger<IArrivedHomeService> logger)
        {
            _logger = logger;
            _redisConnector = redisConnector;
        }

        public async Task<bool> AmIHomeAsync()
        {
            var retValue = false;
            var value = await _redisConnector.GetAsync(IAMHOMEKEY);
            _logger.LogDebug($" ::: ArrivedHomeService.AmIHomeAsync result from redisConnector ::: {value}");
            if (!String.IsNullOrEmpty(value))
                retValue = Boolean.Parse(value);

            return retValue;
        }

        public async Task<bool> IAmHomeAsync(bool toggle)
        {
            var result = await _redisConnector.SetAsync(IAMHOMEKEY, toggle.ToString(), TimeSpan.FromHours(TTL));
            _logger.LogDebug($" ::: ArrivedHomeService.IAmHomeAsync result from redisConnector ::: {result} ::: with set ::: {toggle}");
            return toggle;
        }

        public async Task<long> GetCurrentCountDownTimeAsync()
        {
            var milliseconds = await _redisConnector.GetExpireAsync(IAMHOMEKEY);
            return milliseconds;
        }
    }
}
