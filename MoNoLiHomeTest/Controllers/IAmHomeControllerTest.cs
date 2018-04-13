using System;
using System.Threading.Tasks;
using MoNoLiHome.Controllers;
using MoNoLiHome.Network.Service;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoNoLiHome.Model;

namespace MoNoLiHomeTest.Controllers
{
    [TestClass]
    public class IAmHomeControllerTest
    {

        IArrivedHomeService _arrivedHomeService = Substitute.For<IArrivedHomeService>();
        ILogger<IAmHomeController> _logger = Substitute.For<ILogger<IAmHomeController>>();
        IAmHomeController _controller; 

        public IAmHomeControllerTest()
        {
            _controller = new IAmHomeController(_arrivedHomeService, _logger);
        }

        #region Happy Path

        [TestMethod]
        public async Task When_I_Get_AmIHome_The_Result_Is_Yes()
        {
            _arrivedHomeService.AmIHomeAsync().Returns(true);

            var response = await _controller.Get();
            var value = response.Value;
            var answer = (bool)value.GetType().GetProperty("Answer").GetValue(value);

            Assert.IsTrue(answer);
        }

        [TestMethod]
        public async Task When_I_Get_AmIHome_The_Result_Is_No()
        {
            _arrivedHomeService.AmIHomeAsync().Returns(false);

            var response = await _controller.Get();
            var value = response.Value;
            var answer = (bool)value.GetType().GetProperty("Answer").GetValue(value);

            Assert.IsFalse(answer);
        }

        [TestMethod]
        public async Task When_I_Set_I_Am_Home_Then_I_Am_Home()
        {
            var toggle = true;

            _arrivedHomeService.IAmHomeAsync(toggle).Returns(true);

            var response = await _controller.Post(new IAmHomeMessage() { Toggle = toggle });
            var value = response.Value;
            var answer = (bool)value.GetType().GetProperty("Set").GetValue(value);

            Assert.IsTrue(answer);
        }

        [TestMethod]
        public async Task When_I_Set_I_Am_Not_Home_Then_I_Am_Not_Home()
        {
            var toggle = false;

            _arrivedHomeService.IAmHomeAsync(toggle).Returns(false);

            var response = await _controller.Post(new IAmHomeMessage() { Toggle = toggle });
            var value = response.Value;
            var answer = (bool)value.GetType().GetProperty("Set").GetValue(value);

            Assert.IsFalse(answer);
        }

        #endregion
    }
}
