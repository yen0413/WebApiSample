using System.Data.Common;
using System.Data;
using WebApiSample.IService;
using System.Data.SqlClient;
using Dapper;

namespace WebApiSample.Service
{
    public class DapperService : IDapperService
    {
        public IConfiguration _config;
        int timeout = 60;
        public DapperService(IConfiguration config)
        {
            _config = config;
        }
        public DbConnection GetConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public T Get<T>(string command, object parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetConnection();
            try
            {
                return db.Query<T>(command, parms, commandType: commandType).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public List<T> GetAll<T>(string command, object parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetConnection();
            try
            {
                return db.Query<T>(command, parms, commandType: commandType, commandTimeout: timeout).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public int Execute(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = GetConnection();
            try
            {
                return db.Execute(sp, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
