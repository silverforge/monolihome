using System.Threading.Tasks;

namespace MoNoLiHome.Network.Client
{
    public interface IFirebaseConnector
    {
        Task<bool> PingMotionDetectedAsync();
    }
}
