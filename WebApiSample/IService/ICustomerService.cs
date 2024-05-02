using WebApiSample.Model;

namespace WebApiSample.IService
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<List<Customers>> GetCustomerByID(Customers _Customer);
    }
}
