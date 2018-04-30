using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MoNoLiHome.Network.Client
{
    public class RedisConnector : IRedisConnector
    {
        IDatabase _redisStore;

        public RedisConnector(IConnectionMultiplexer redis)
        {
            _redisStore = redis.GetDatabase();
        }

        public async Task<string> GetAsync(string key)
        {
            return await _redisStore.StringGetAsync(key);
        }

        public async Task<bool> SetAsync(string key, string value, TimeSpan expire)
        {
            return await _redisStore.StringSetAsync(key, value, expire);
        }

        public async Task<long> GetExpireAsync(string key)
        {
            var result = await _redisStore.StringGetWithExpiryAsync(key);
            if (result.Expiry == null)
                return 0;

            return Convert.ToInt64(result.Expiry.Value.Ticks);
        }
    }
}
