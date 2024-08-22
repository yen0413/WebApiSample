using Castle.Core.Logging;
using Castle.Core.Resource;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using WebApiSample.Controllers;
using WebApiSample.IService;
using WebApiSample.Model;
using WebApiSample.Service;

namespace WebApiSampleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestCustomerID()
        {
            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(x => x.GetAllCustomers().Result).Returns(
                new List<Customers>()
                {
                    new Customers(){ CustomerID = "Rex" }
                });
            var sysGroupObject = new CustomerController(mockRepo.Object);
            var returnData = await sysGroupObject.GetAllCustomers();
            Assert.IsTrue(returnData.FirstOrDefault()?.CustomerID == "R**", "CustomerID Error");
        }

        [TestMethod]
        public async Task GetCustomerByIDIsExisit()
        {
            var mockRepo = new Mock<ICustomerService>();
            var expectedCustomer = new Customers { CustomerID = "ALFKI" };

            mockRepo.Setup(x => x.GetCustomerByIDWithResults("").Result).Returns(
                   expectedCustomer
               );
            var sysGroupObject = new CustomerController(mockRepo.Object);
            var result = await sysGroupObject.GetCustomerByIDWithResults("");
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(Ok<Customers>), "The result is not of type Ok<Customers>.");

            var okResult = (result.Result) as Ok<Customers>;
            Assert.IsNotNull(okResult, "Customer is not exisit");
            Assert.AreEqual(expectedCustomer.CustomerID, "ALFKI", "CustomerID ALFKI is not correct");
        }
        [TestMethod]
        public async Task GetCustomerByIDIsNotExisit()
        {
            var mockRepo = new Mock<ICustomerService>();
            //var expectedCustomer = new Customers();

            mockRepo.Setup(x => x.GetCustomerByIDWithResults("").Result).Returns(
                   (Customers?)null
               );
            var sysGroupObject = new CustomerController(mockRepo.Object);
            var result = await sysGroupObject.GetCustomerByIDWithResults("");
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFound), "The result is NotFound.");
        }
    }
}