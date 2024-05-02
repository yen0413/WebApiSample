using WebApiSample.Model;

namespace WebApiSample.IService
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<Customers> GetCustomerByID(Customers _Customer);
        Task<int> UpdateCustomerByID(Customers _Customer);
    }
}
