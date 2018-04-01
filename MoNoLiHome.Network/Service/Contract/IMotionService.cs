using System;
using System.Threading.Tasks;

namespace MoNoLiHome.Network.Service
{
    public interface IMotionService
    {
        Task<bool> DetectedAsync();
    }
}
