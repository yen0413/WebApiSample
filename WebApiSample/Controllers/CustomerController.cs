using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiSample.IService;
using WebApiSample.Model;

namespace WebApiSample.Controllers
{
    [Authorize]
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
        [HttpGet("{id}")]
        //[ProducesResponseType<Customers>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<Customers>, NotFound>> GetCustomerByIDWithResults(string CustomerID)
        {
            var Customer = await _ICustomerService.GetCustomerByIDWithResults(CustomerID);
            return Customer == null ? TypedResults.NotFound() : TypedResults.Ok(Customer);
        }

        [Authorize(Roles = "Admin")]
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
