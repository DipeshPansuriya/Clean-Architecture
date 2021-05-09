using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IDapper : IDisposable
    {
        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType);

        Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType);
    }
}