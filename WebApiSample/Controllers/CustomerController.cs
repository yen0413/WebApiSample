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
        [HttpPost("GetCustomerByID")]
        public Task<List<Customers>> GetCustomerByID(Customers customer)
        {
            return _ICustomerService.GetCustomerByID(customer);
        }
    }
}
