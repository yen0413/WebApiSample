using System.Data;

namespace WebApiSample.IService
{
    public interface IDapperService
    {
        T Get<T>(string command, object parms, CommandType commandType = CommandType.Text);
        List<T> GetAll<T>(string command, object parms, CommandType commandType = CommandType.Text);
        int Execute(string sp, object parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
