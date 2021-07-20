using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IDapper : IDisposable
    {
        Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<int> ExecuteAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<string> ExecuteScalarAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<List<T>> GetDataAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<List<T>> GetDataAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType);

        DataSet ConvertDataReaderToDataSet(IDataReader data);
    }
}