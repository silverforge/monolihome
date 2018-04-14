using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoNoLiHome.Network.Client;
using NSubstitute;
using MoNoLiHome.Network.Service;
using System.Threading.Tasks;

namespace MoNoLiHome.NetworkTest.Service
{
    [TestClass]
    public class MotionServiceTest
    {
        readonly IFirebaseConnector _firebaseConnector = Substitute.For<IFirebaseConnector>();
        readonly IMotionService _motionService;

        public MotionServiceTest()
        {
            _motionService = new MotionService(_firebaseConnector);
        }

        #region Happy Path

        [TestMethod]
        public async Task MotionDetectedNotificationSentSuccessfully()
        {
            _firebaseConnector.PingMotionDetectedAsync().Returns(true);

            var result = await _motionService.DetectedAsync();
            Assert.IsTrue(result);
        }

        #endregion

        #region Sad Path

        [TestMethod]
        public async Task MotionDetectedNotificationSendFailed()
        {
            _firebaseConnector.PingMotionDetectedAsync().Returns(false);

            var result = await _motionService.DetectedAsync();
            Assert.IsFalse(result);
        }

        #endregion
    }
}
