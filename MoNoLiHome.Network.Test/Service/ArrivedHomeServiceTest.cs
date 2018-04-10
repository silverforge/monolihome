using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MoNoLiHome.Network.Service;
using NSubstitute;
using MoNoLiHome.Network.Client;
using Microsoft.Extensions.Logging;

namespace MoNoLiHome.NetworkTest.Service
{
    [TestClass]
    public class ArrivedHomeServiceTest
    {
        readonly IRedisConnector _redisConnector = Substitute.For<IRedisConnector>();
        readonly ILogger<IArrivedHomeService> _logger = Substitute.For<ILogger<IArrivedHomeService>>();
        readonly IArrivedHomeService _arrivedHomeService;

        public ArrivedHomeServiceTest()
        {
            _arrivedHomeService = new ArrivedHomeService(_redisConnector, _logger);
        }

        #region Happy Path

        [TestMethod]
        public async Task AmIHomeYes()
        {
            _redisConnector.GetAsync(Arg.Any<string>()).Returns("true");

            var result = await _arrivedHomeService.AmIHomeAsync();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AmIHomeNo()
        {
            _redisConnector.GetAsync(Arg.Any<string>()).Returns("false");

            var result = await _arrivedHomeService.AmIHomeAsync();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task IAmHomeSetYes()
        {
            var value = true;

            _redisConnector.SetAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<TimeSpan>()).Returns(value);
            var result = await _arrivedHomeService.IAmHomeAsync(value);

            await _redisConnector.Received().SetAsync(Arg.Any<string>(), value.ToString(), Arg.Any<TimeSpan>());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IAmHomeSetNo()
        {
            var value = false;

            _redisConnector.SetAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<TimeSpan>()).Returns(value);
            var result = await _arrivedHomeService.IAmHomeAsync(value);

            await _redisConnector.Received().SetAsync(Arg.Any<string>(), value.ToString(), Arg.Any<TimeSpan>());
            Assert.IsFalse(result);
        }

        #endregion

        #region Sad Path

        [TestMethod]
        public async Task AmIHomeEmpty()
        {
            _redisConnector.GetAsync(Arg.Any<string>()).Returns(String.Empty);

            var result = await _arrivedHomeService.AmIHomeAsync();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AmIHomeNull()
        {
            _redisConnector.GetAsync(Arg.Any<string>()).Returns((string)null);

            var result = await _arrivedHomeService.AmIHomeAsync();
            Assert.IsFalse(result);
        }

        #endregion
    }
}
