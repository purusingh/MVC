using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReeDirectory.Controllers;

namespace ReeDirectoryTest
{
    [TestClass]
    public class CountryControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x=>x.HttpContext.User.Identity.Name).Returns("USDA\\Purushottam.Singh");
            mock.SetupGet(x=>x.HttpContext.Request.IsAuthenticated).Returns(true);
            
            CountryController country = new CountryController();
            country.ControllerContext = mock.Object;

            ViewResult result = country.Index() as ViewResult;
            Assert.IsNotNull(result);

        }
    }
}
