using Microsoft.AspNetCore.Mvc;
using WebApiSample.IService;
using WebApiSample.Model;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _ICustomerService;
        public CustomerController(ICustomerService customerService)
        {
            _ICustomerService = customerService;
        }
        [HttpGet("GetAllCustomer")]
        public Task<List<Customers>> GetAllCustomers()
        {
            try
            {
                return _ICustomerService.GetAllCustomers();
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<Customers>());
                throw;
            }
        }
        [HttpGet("{CustomerID:alpha}")]
        public Task<Customers> GetCustomerByID(string CustomerID)
        {
            try
            {
                return _ICustomerService.GetCustomerByID(CustomerID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPut("UpdateCustomerByID")]
        public Task<string> UpdateCustomerByID(Customers customer)
        {
            try
            {
                var updateResult = _ICustomerService.UpdateCustomerByID(customer).Result;
                string responseMsg;
                if (updateResult > 0)
                {
                    responseMsg = "update success";
                }
                else
                {
                    responseMsg = "update fail";
                }
                return Task.FromResult(responseMsg);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpDelete("{CustomerID:alpha}")]
        public Task<string> DeleteCustomerByID(string CustomerID)
        {
            try
            {
                var updateResult = _ICustomerService.DeleteCustomerByID(CustomerID).Result;
                string responseMsg;
                if (updateResult > 0)
                {
                    responseMsg = "delete success";
                }
                else
                {
                    responseMsg = "delete fail";
                }
                return Task.FromResult(responseMsg);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
