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
        CountryController controller;

        public CountryControllerTest()
        {

            var mockHttpContext = new Mock<ControllerContext>();
            mockHttpContext.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(user);
            mockHttpContext.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);


            List<ECountry> countries = new List<ECountry> { new ECountry { Id = 11 } };
            List<ESecurity> securities = new List<ESecurity> { new ESecurity { Add = 0 } };

            // Arrange            
            var mockRepository = new Mock<IReeRepository<ECountry>>();

            int? totalRecord = 0;
            mockRepository.Setup(r => r.SelectAll(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>(), ref totalRecord, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(countries);
            mockRepository.Setup(x => x.SqlQuery<ESecurity>(It.IsAny<string>(), It.IsAny<object[]>())).Returns(securities);


            mockRepository.Setup(x => x.Insert(It.IsAny<ECountry>())).Returns((ECountry entity) => 
            {
                //ECountry cty = countries.FirstOrDefault(ext => ext.Id == entity.Id);
                //if (cty == null)
                //{
                //    //cty.Name = entity.Name;
                //    return false;
                //}
                //else
                //    countries.Add(cty);

                if (entity.Id.Equals(default(int)))
                {
                    countries.Add(entity);
                }
                return entity;

            });


            controller = new CountryController();
            controller.ControllerContext = mockHttpContext.Object;
            controller.db = mockRepository.Object;

        }


        [TestMethod]
        public void IndexTest0()
        {
            // Act            
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Error Index is returning null value");
            Assert.AreEqual(result.ViewName, "Index", "It is returning different view");
            Assert.IsNotNull(result.ViewBag.Security, "It is returning different view");
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
           
            // Act            
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Error Index is returning null value");
            Assert.AreEqual(result.ViewName, "Index", "It is returning different view");
           // Assert.AreEqual(result.ViewBag.Security, securities[0], "It is returning different view");
                        
        }

        [TestMethod] 
        public void ListActionReturnsListOfCategories() 
        {
            
            Country country = new Country();
            // Act            
            ViewResult result = controller.Create(new ECountry()) as ViewResult;            
            // Assert
            //Assert.IsNotNull(result, "Error Edit is returning null value");
            //Assert.AreEqual(result.ViewName, "Edit", "It is returning different view");

            result = controller.Index() as ViewResult; ;

            Assert.AreEqual(2, ((Country)result.Model).Entities.Count);
        }


    }
}
