using Application_Common;
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

        Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<IEnumerable<T>> GetDataListAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<T> GetDataFirstorDefaultAsync<T>(string Query, DynamicParameters param, CommandType commandType);

        Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType);

        Task<dynamic> RunByQueryMultiple(string Query, DynamicParameters param, CommandType commandType, IEnumerable<MapItem> mapItems = null);
    }
}