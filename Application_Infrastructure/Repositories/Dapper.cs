using Application_Common;
using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Application_Infrastructure.Repositories
{
    public class Dapper<T> : IDapper<T> where T : class
    {
        private readonly string msg = string.Empty;
        private readonly ILogger<Dapper<T>> _logger;
        private readonly IBackgroundJob _backgroundJob;
        private readonly ICacheService _cache;

        public Dapper(ILogger<Dapper<T>> logger, IBackgroundJob backgroundClient, ICacheService cache)
        {
            _logger = logger;
            _backgroundJob = backgroundClient;
            _cache = cache;
        }

        public void Dispose()
        {
        }

        private IDbConnection dbConnection()
        {
            IDbConnection db = new SqlConnection(APISetting.DBConnection);
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }

            return db;
        }

        public async Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                int affectedRows = 0;

                using (IDbConnection db = dbConnection())
                {
                    affectedRows = param == null ? await db.ExecuteAsync(Query, commandType: commandType) : await db.ExecuteAsync(Query, param, commandType: commandType);
                }
                return affectedRows;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string data = string.Empty;

                using (IDbConnection db = dbConnection())
                {
                    data = param == null ? await db.ExecuteScalarAsync<string>(Query, commandType: commandType) : await db.ExecuteScalarAsync<string>(Query, param, commandType: commandType);
                }
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetDataListAsync<T>(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                using (IDbConnection db = dbConnection())
                {
                    IEnumerable<T> data = param == null ? await db.QueryAsync<T>(Query, commandType: commandType) : await db.QueryAsync<T>(Query, param, commandType: commandType);

                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<T> GetDataFirstorDefaultAsync<T>(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                using (IDbConnection db = dbConnection())
                {
                    IEnumerable<T> data = param == null ? await db.QueryAsync<T>(Query, commandType: commandType) : await db.QueryAsync<T>(Query, param, commandType: commandType);

                    return data.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<dynamic> RunByQueryMultiple(string Query, DynamicParameters param, CommandType commandType, IEnumerable<MapItem> mapItems = null)
        {
            ExpandoObject data = new ExpandoObject();

            if (mapItems == null)
            {
                return data;
            }

            try
            {
                using (IDbConnection db = dbConnection())
                {
                    GridReader multi = await db.QueryMultipleAsync(Query, param, commandType: commandType);

                    foreach (MapItem item in mapItems)
                    {
                        if (item.DataRetriveType == DataRetriveTypeEnum.FirstOrDefault)
                        {
                            object singleItem = multi.Read(item.Type).FirstOrDefault();
                            ((IDictionary<string, dynamic>)data).Add(item.PropertyName, singleItem);
                        }

                        if (item.DataRetriveType == DataRetriveTypeEnum.ToList)
                        {
                            List<object> listItem = multi.Read(item.Type).ToList();
                            ((IDictionary<string, dynamic>)data).Add(item.PropertyName, listItem);
                        }
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                using (IDbConnection db = dbConnection())
                {
                    IDataReader list = param == null ? await db.ExecuteReaderAsync(Query, commandType: commandType) : await db.ExecuteReaderAsync(Query, param, commandType: commandType);

                    DataSet dataset = ConvertDataReaderToDataSet(list);
                    return dataset;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        private DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            DataSet ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table");
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }
            return ds;
        }
    }
}