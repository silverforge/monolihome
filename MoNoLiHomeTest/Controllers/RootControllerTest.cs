using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoNoLiHome.Controllers;
using MoNoLiHomeTest.Util;

namespace MoNoLiHomeTest.Controllers
{
    [TestClass]
    public class RootControllerTest
    {

        RootController _rootController = new RootController();

        #region Happy Path

        [TestMethod]
        public void When_Root_Get_Then_Version_Info_Responded()
        {
            var result = _rootController.Get();
            var value = result.Value;
            var name = value.GetPropertyValue<string>("Name");
            var version = value.GetPropertyValue<string>("Version");

            Assert.AreEqual(name.ToLower(), "monolihome");
            Assert.AreEqual(version.ToLower(), "1.0");
        }

        #endregion
    }
}
