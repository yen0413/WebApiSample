using Microsoft.AspNetCore.Authorization;
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
                var allCustomerList = _ICustomerService.GetAllCustomers();
                foreach (var item in allCustomerList.Result)
                {
                    item.CustomerID = item.CustomerID
                                          .Substring(0, item.CustomerID.Length - 2) + "**";
                }

                return allCustomerList;
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<Customers>());
                throw;
            }
        }
        [Authorize]
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
        [Authorize]
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
            catch (Exception ex )
            {
                responseMsg = $"update fail ; errorMsg : {ex.Message}";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }

        [Authorize]
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
            catch (Exception ex)
            {
                responseMsg = $"delete fail  ; errorMsg : {ex.Message}";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }
        [Authorize]
        [HttpPost("PostCustomer")]
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
            catch (Exception ex)
            {
                responseMsg = $"post fail ; errorMsg : {ex.Message}";
                //throw;
            }
            return Task.FromResult(responseMsg);
        }
    }
}
