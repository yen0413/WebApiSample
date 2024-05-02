using Dapper;
using System.Data;
using WebApiSample.IService;
using WebApiSample.Model;

namespace WebApiSample.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IDapperService _dapperService;
        public CustomerService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }
        public Task<List<Customers>> GetAllCustomers()
        {
            DynamicParameters dbPara = new();
            string sql = @"SELECT * FROM Customers";
            var result = _dapperService.GetAll<Customers>(sql, dbPara, commandType: CommandType.Text);

            return Task.FromResult(result);
        }
        public Task<List<Customers>> GetCustomerByID(Customers _Customer)
        {
            string sql = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
            var result = _dapperService.GetAll<Customers>(sql, _Customer, commandType: CommandType.Text);
            return Task.FromResult(result);
        }
    }
}
