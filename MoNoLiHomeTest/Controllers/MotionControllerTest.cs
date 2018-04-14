using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoNoLiHome.Controllers;
using MoNoLiHome.Network.Service;
using NSubstitute;
using System.Threading.Tasks;
using MoNoLiHome.Model.Request;
using MoNoLiHomeTest.Util;

namespace MoNoLiHomeTest.Controllers
{
    [TestClass]
    public class MotionControllerTest
    {
        readonly IArrivedHomeService _homeService = Substitute.For<IArrivedHomeService>();
        readonly IMotionService _motionService = Substitute.For<IMotionService>();
        readonly ILogger<MotionController> _logger = Substitute.For<ILogger<MotionController>>();
        readonly MotionController _motionController;

        public MotionControllerTest()
        {
            _motionController = new MotionController(_homeService, _motionService, _logger);
        }

        #region Happy Path

        [TestMethod]
        public void When_I_Get_Then_I_Receive_Response()
        {
            var result = _motionController.Get();
            var value = result.Value;
            var message = value.GetPropertyValue<string>("Message");

            Assert.AreEqual(message.ToLower(), "motion");

        }

        [TestMethod]
        public async Task When_Motion_Detected_And_I_Am_Not_Home_Then_Cloud_Function_Pinged()
        {
            _homeService.AmIHomeAsync().Returns(false);
            _motionService.DetectedAsync().Returns(true);

            var result = await _motionController.PostMotionDetected(new MotionDetectedMessage { Alert = true });

            var value = result.Value;
            var response = value.GetPropertyValue<bool>("Response");
            var messageSent = value.GetPropertyValue<bool>("MessageSent");

            Assert.IsTrue(response);
            Assert.IsTrue(messageSent);
        }

        [TestMethod]
        public async Task When_Motion_Detected_And_I_Am_Home_Then_Cloud_Function_Is_Not_Pinged()
        {
            _homeService.AmIHomeAsync().Returns(true);

            var result = await _motionController.PostMotionDetected(new MotionDetectedMessage { Alert = true });

            var value = result.Value;
            var response = value.GetPropertyValue<bool>("Response");
            var messageSent = value.GetPropertyValue<bool>("MessageSent");

            Assert.IsFalse(response);
            Assert.IsFalse(messageSent);
        }

        #endregion
    }
}
