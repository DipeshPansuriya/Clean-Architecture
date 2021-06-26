using Application_Core.Repositories;
using Application_Domain;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string msg = string.Empty;
        private readonly ILogger<Dapperr> _logger;

        public Dapperr(IConfiguration config, ILogger<Dapperr> logger)
        {
            this._config = config;
            this._logger = logger;
        }

        public void Dispose()
        {
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using IDbConnection db = new SqlConnection(APISetting.DBConnection);
            IEnumerable<T> data = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return data.FirstOrDefault();
        }

        public async Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using IDbConnection db = new SqlConnection(APISetting.DBConnection);

            IEnumerable<T> data = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return data.ToList();
        }
    }
}