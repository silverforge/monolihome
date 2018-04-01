using System;
using System.Threading.Tasks;
using MoNoLiHome.Network.Client;
namespace MoNoLiHome.Network.Service
{
    public class MotionService : IMotionService
    {
        readonly IFirebaseConnector _firebaseConnector;

        public MotionService(IFirebaseConnector firebaseConnector)
        {
            _firebaseConnector = firebaseConnector;
        }

        public async Task<bool> DetectedAsync()
        {
            return await _firebaseConnector.PingMotionDetectedAsync();
        }
    }
}
