using Dapper;
using System.Data;

namespace Application_Core.Repositories
{
    public interface IDapper<T> : IDisposable
    {
        void Dispose();

        Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<IEnumerable<T>> GetDataListAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType);
    }
}