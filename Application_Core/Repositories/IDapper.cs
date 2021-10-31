using Application_Genric;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IDapper<T> : IDisposable
    {
        void Dispose();

        Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<int> ExecuteAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<string> ExecuteScalarAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);
        Task<IEnumerable<T>> GetDataAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<IEnumerable<T>> GetDataAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType);

       Task<T> GetDataFirstorDefaultAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<T> GetDataFirstorDefaultAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType);
        
        DataSet ConvertDataReaderToDataSet(IDataReader data);
        Task<int> SaveAsync(T entity, bool IsCache, string Cahekey);

        Task<int> SaveAsync(T entity);

        Task<int> SaveNotificationAsync(NotficationCls entity);

        Task DeleteAsync(T entity, bool IsCache, string Cahekey);

        Task<bool> DeleteAsync(T entity);

        Task<int> UpdateAsync(T entity, bool IsCache, string Cahekey);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateNotificationAsync(NotficationCls entity);

        Task<T> GetDetails(int id);
    }
}