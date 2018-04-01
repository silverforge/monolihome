using System;
using System.Threading.Tasks;

namespace MoNoLiHome.Network.Client
{
    public interface IRedisConnector
    {
        Task<string> GetAsync(string key);
        Task<bool> SetAsync(string key, string value, TimeSpan expire);
    }
}
