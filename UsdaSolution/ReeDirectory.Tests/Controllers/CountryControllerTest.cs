using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ReeDirectory.Controllers;
using ReeDirectoryEntityFm.Entities.General;
using ReeDirectoryEntityFm.ExternalEntity;
using ReeDirectoryEntityFm.Repositories;

namespace ReeDirectory.Tests.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        readonly string user = ConfigurationManager.AppSettings["User"].ToString();

        [TestMethod]
        public void IndexTest0()
        {
            // Arrange
            var  mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            //var mockSecurity = new Mock<ESecurity>();
            var mockRepository = new Mock<IReeRepository<ECountry>>();
             //var mockSecurity = new Mock<List<ESecurity>>();
            //mockRepository.Setup(db => db.SqlQuery<ESecurity>(null)).Returns(mockSecurity.Object);


            Mock<CountryController> mockController = new Mock<CountryController>();
            mockController.Object.ControllerContext = mockHttpContext.Object;
            mockController.Protected().SetupGet<ESecurity>("Security").Returns(new ESecurity{ Add=0});
            mockController.Protected().SetupGet<IReeRepository<ECountry>>("db").Returns(mockRepository.Object);

            CountryController controller = mockController.Object;
            //controller.ControllerContext = mockHttpContext.Object;

            //CountryController controller = new CountryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexTest1()
        {
            // Arrange
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            var mockSecurity = new Mock<List<ESecurity>>();
            var mockRepository = new Mock<IReeRepository<ECountry>>();
            mockRepository.Setup(db => db.SqlQuery<ESecurity>(null)).Returns(mockSecurity.Object);

            CountryController controller = new CountryController();

            controller.ControllerContext = mockHttpContext.Object;
            controller.db = mockRepository.Object;            
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
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            CountryController controller = new CountryController();

            controller.ControllerContext = mock.Object;
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert            
            ESecurity actual =result.ViewData["Security"] as ESecurity;
            ESecurity mustBe =new ESecurity { Add = 1, Edit = 1, Delete = 0, Print = 1 };
            Assert.IsNotNull(actual, "Controller Permission is not set yet");
            //Assert.AreEqual(actual.Add,mustBe.Add, "Add is not correct");
            //Assert.AreEqual(actual.Edit, mustBe.Edit, "Edit is not correct");
            //Assert.AreEqual(actual.Delete, mustBe.Delete, "Delete is not correct");
            //Assert.AreEqual(actual.Print, mustBe.Print, "print is not correct");
            
                        
        }

        [TestMethod] 
        public void ListActionReturnsListOfCategories() 
        { // Arrange // create the mock 
            var mock = new Mock<IReeRepository<ECountry>>(); 
            // create a list of categories to return 
            List<ECountry> countries = new List<ECountry> { new ECountry {Id = 1, Name = "test"} }; 
            // tell the mock that when FindAll is called, 
            // return the list of categories 
            mock.Setup(cr => cr.SelectAll()).Returns(countries); 
            // pass the mocked instance, not the mock itself, to the category 
            // controller using the Object property 
            //var controller = Mock<CountryController>(mock.Object);
            //controller.ControllerContext = mock.Object; 
            // Act 
            //var result = (ViewResult) controller.Index(); 
            // Assert 
            //var listCountries = Assert.i .IsAssignableFrom<IEnumerable<ECountry>>(result.ViewData.Model);
            //Assert.Equal(1, listCountries.Count());
        }

    }
}
