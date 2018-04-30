using System;
using System.Threading.Tasks;

namespace MoNoLiHome.Network.Service
{
    public interface IArrivedHomeService
    {
        Task<bool> AmIHomeAsync();
        Task<bool> IAmHomeAsync(bool toggle);
        Task<long> GetCurrentCountDownTimeAsync();
    }
}
