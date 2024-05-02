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
        public Task<Customers> GetCustomerByID(Customers _Customer)
        {
            string sql = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
            var result = _dapperService.Get<Customers>(sql, _Customer, commandType: CommandType.Text);
            return Task.FromResult(result);
        }
        public Task<int> UpdateCustomerByID(Customers _Customer) 
        {
            string sql = @"UPDATE Customers SET 
                            [CompanyName] = @CompanyName
                           ,[ContactName] = @ContactName
                           ,[ContactTitle] = @ContactTitle
                           ,[Address] = @Address
                           ,[City] = @City
                           ,[Region] = @Region
                           ,[PostalCode] = @PostalCode
                           ,[Country] = @Country
                           ,[Phone] = @Phone
                           ,[Fax] = @Fax
	                       WHERE [CustomerID] = @CustomerID";
            var result = _dapperService.Execute(sql, _Customer, commandType: CommandType.Text);
            return Task.FromResult(result);
        }
    }
}
