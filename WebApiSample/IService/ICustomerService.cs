using WebApiSample.Model;

namespace WebApiSample.IService
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<Customers> GetCustomerByID(string CustomerID);
        Task<int> UpdateCustomerByID(Customers _Customer);
        Task<int> DeleteCustomerByID(string CustomerID);
    }
}
