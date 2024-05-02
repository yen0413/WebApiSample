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
            string responseMsg;
            try
            {
                var updateResult = _ICustomerService.UpdateCustomerByID(customer).Result;
                if (updateResult > 0)
                {
                    responseMsg = "update success";
                }
                else
                {
                    responseMsg = "update fail";
                }
            }
            catch (Exception)
            {
                responseMsg = "update fail";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }
        [HttpDelete("{CustomerID:alpha}")]
        public Task<string> DeleteCustomerByID(string CustomerID)
        {
            string responseMsg;
            try
            {
                var updateResult = _ICustomerService.DeleteCustomerByID(CustomerID).Result;
                if (updateResult > 0)
                {
                    responseMsg = "delete success";
                }
                else
                {
                    responseMsg = "delete fail";
                }
            }
            catch (Exception)
            {
                responseMsg = "delete fail";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }
        [HttpPost]
        public Task<string> PostCustomer(Customers customers)
        {
            string responseMsg;
            try
            {
                var result = _ICustomerService.PostCustomer(customers).Result;
                if (result > 0)
                {
                    responseMsg = "post success";
                }
                else
                {
                    responseMsg = "post fail";
                }
            }
            catch (Exception)
            {
                responseMsg = "post fail";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }
    }
}
