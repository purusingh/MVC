using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ReeDirectory.Controllers;
using ReeDirectory.Models;
using ReeDirectoryEntityFm.Entities.General;
using ReeDirectoryEntityFm.ExternalEntity;
using ReeDirectoryEntityFm.Repositories;
using System.Linq;

namespace ReeDirectory.Tests.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        readonly string user = ConfigurationManager.AppSettings["User"].ToString();

        [TestMethod]
        public void IndexTest0()
        {

            List<ECountry> countries = new List<ECountry>{ new ECountry{ Id=11}};
            List<ESecurity> securities = new List<ESecurity> { new ESecurity { Add = 0 } };


            // Arrange
            var  mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            

            var mockRepository = new Mock<IReeRepository<ECountry>>();
            int? totalRecord = 0;
            mockRepository.Setup(r => r.SelectAll(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>(), ref totalRecord, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(countries);
            mockRepository.Setup(x => x.SqlQuery<ESecurity>(It.IsAny<string>(), It.IsAny<object[]>())).Returns(securities);

            List<ESecurity> sec = mockRepository.Object.SqlQuery<ESecurity>("","");
            CountryController controller = new CountryController();


            controller.ControllerContext = mockHttpContext.Object;
            controller.db = mockRepository.Object;
            

            // Act            
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Error Index is returning null value");
            Assert.AreEqual(result.ViewName, "Index", "It is returning different view");
            Assert.AreEqual(result.ViewBag.Security, securities[0], "It is returning different view");
        }

        [TestMethod]
        public void IndexTest1()
        {
            List<ECountry> countries = new List<ECountry> { new ECountry { Id = 11 } };
            List<ESecurity> securities = new List<ESecurity> { new ESecurity { Add = 0 } };


            // Arrange
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);


            var mockRepository = new Mock<IReeRepository<ECountry>>();
            int? totalRecord = 0;
            mockRepository.Setup(r => r.SelectAll(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>(), ref totalRecord, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(countries);
            mockRepository.Setup(x => x.SqlQuery<ESecurity>(It.IsAny<string>(), It.IsAny<object[]>())).Returns(securities);

            List<ESecurity> sec = mockRepository.Object.SqlQuery<ESecurity>("", "");
            CountryController controller = new CountryController();
            
            controller.ControllerContext = mockHttpContext.Object;
            controller.db = mockRepository.Object;

            Country country = new Country();

            // Act            
            ViewResult result = controller.Index(country) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Error Index is returning null value");
            Assert.AreEqual(result.ViewName, "Index", "It is returning different view");
            Assert.AreEqual(result.ViewBag.Security, securities[0], "Security is not set");
        }

        [TestMethod]
        public void IndexTest2()
        {
            List<ECountry> countries = new List<ECountry> { new ECountry { Id = 11 }, new ECountry { Name = "ddd" } };
            List<ESecurity> securities = new List<ESecurity> { new ESecurity { Add = 0 } };


            // Arrange
            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);


            var mockRepository = new Mock<IReeRepository<ECountry>>();
            int? totalRecord = 0;
           // mockRepository.Setup(r => r.SelectAll(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>(), ref totalRecord, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns((string filterval) => countries.Where(e => e.Name == filterval).ToList());
            mockRepository.Setup(x => x.SqlQuery<ESecurity>(It.IsAny<string>(), It.IsAny<object[]>())).Returns(securities);

            List<ESecurity> sec = mockRepository.Object.SqlQuery<ESecurity>("", "");
            CountryController controller = new CountryController();


            controller.ControllerContext = mockHttpContext.Object;
            controller.db = mockRepository.Object;

            Country country = new Country();            
            // Act            
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Error Index is returning null value");
            Assert.AreEqual(result.ViewName, "Index", "It is returning different view");
            Assert.AreEqual(result.ViewBag.Security, securities[0], "It is returning different view");
                        
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
