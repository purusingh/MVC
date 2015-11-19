using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReeDirectory;
using ReeDirectory.Controllers;
using ReeDirectory.EntityFM.ExternalEntity;

namespace ReeDirectory.Tests.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        [TestMethod]
        public void IndexTest1()
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

        [TestMethod]
        public void IndexTest2()
        {
            // Arrange
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("USDA\\Purushottam.Singh");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            CountryController controller = new CountryController();

            controller.ControllerContext = mock.Object;
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            ESecurity actual =result.ViewData["Security"] as ESecurity;
            ESecurity mustBe =new ESecurity { Add = 1, Edit = 1, Delete = 0, Print = 1 }; 
            Assert.AreEqual(actual.Add,mustBe.Add, "Add is not correct");
            Assert.AreEqual(actual.Edit, mustBe.Edit, "Edit is not correct");
            Assert.AreEqual(actual.Delete, mustBe.Delete, "Delete is not correct");
            Assert.AreEqual(actual.Print, mustBe.Print, "print is not correct");
            
                        
        }        
    }
}
