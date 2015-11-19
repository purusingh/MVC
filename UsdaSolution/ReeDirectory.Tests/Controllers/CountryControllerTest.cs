using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReeDirectory;
using ReeDirectory.Controllers;

namespace ReeDirectory.Tests.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var  mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("USDA\\Purushottam.Singh");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
                
            CountryController controller = new CountryController();

            controller.ControllerContext = mock.Object;
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }        
    }
}
