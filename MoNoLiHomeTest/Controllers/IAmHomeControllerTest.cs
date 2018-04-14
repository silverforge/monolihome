using System;
using System.Threading.Tasks;
using MoNoLiHome.Controllers;
using MoNoLiHome.Network.Service;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoNoLiHome.Model;
using MoNoLiHomeTest.Util;

namespace MoNoLiHomeTest.Controllers
{
    [TestClass]
    public class IAmHomeControllerTest
    {
        readonly IArrivedHomeService _arrivedHomeService = Substitute.For<IArrivedHomeService>();
        readonly ILogger<IAmHomeController> _logger = Substitute.For<ILogger<IAmHomeController>>();
        readonly IAmHomeController _controller;

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
            var answer = value.GetPropertyValue<bool>("Answer");

            Assert.IsTrue(answer);
        }

        [TestMethod]
        public async Task When_I_Get_AmIHome_The_Result_Is_No()
        {
            _arrivedHomeService.AmIHomeAsync().Returns(false);

            var response = await _controller.Get();
            var value = response.Value;
            var answer = value.GetPropertyValue<bool>("Answer");

            Assert.IsFalse(answer);
        }

        [TestMethod]
        public async Task When_I_Set_I_Am_Home_Then_I_Am_Home()
        {
            var toggle = true;

            _arrivedHomeService.IAmHomeAsync(toggle).Returns(true);

            var response = await _controller.Post(new IAmHomeMessage() { Toggle = toggle });
            var value = response.Value;
            var set = value.GetPropertyValue<bool>("Set");

            Assert.IsTrue(set);
        }

        [TestMethod]
        public async Task When_I_Set_I_Am_Not_Home_Then_I_Am_Not_Home()
        {
            var toggle = false;

            _arrivedHomeService.IAmHomeAsync(toggle).Returns(false);

            var response = await _controller.Post(new IAmHomeMessage() { Toggle = toggle });
            var value = response.Value;
            var set = value.GetPropertyValue<bool>("Set");

            Assert.IsFalse(set);
        }

        #endregion
    }
}
