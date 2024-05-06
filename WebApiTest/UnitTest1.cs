using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.Metrics;
using WebApiSample.Controllers;
using WebApiSample.IService;
using WebApiSample.Model;
using WebApiSample.Service;

namespace WebApiTest
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
    }
}