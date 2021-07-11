using Application_Core.Repositories;
using Application_Domain;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IGetQuery _query;

        public Dapperr(IConfiguration config, ILogger<Dapperr> logger, IGetQuery query)
        {
            this._config = config;
            this._logger = logger;
            this._query = query;
        }

        public void Dispose()
        {
        }

        public async Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            int affectedRows = 0;

            using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                try
                {
                    if (param == null)
                    {
                        affectedRows = await db.ExecuteAsync(Query, commandType: commandType);
                    }
                    else
                    {
                        affectedRows = await db.ExecuteAsync(Query, param, commandType: commandType);
                    }
                }
                catch (Exception)
                {
                }
            }
            return affectedRows;
        }

        public async Task<int> ExecuteAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            string DBQuery = this._query.GetDBQuery(foldername, queryID, param);
            int affectedRows = 0;
            if (!string.IsNullOrWhiteSpace(DBQuery))
            {
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    try
                    {
                        if (param == null)
                        {
                            affectedRows = await db.ExecuteAsync(DBQuery, commandType: commandType);
                        }
                        else
                        {
                            affectedRows = await db.ExecuteAsync(DBQuery, param, commandType: commandType);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return affectedRows;
        }

        public async Task<string> ExecuteScalarAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            string data = string.Empty;

            using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                try
                {
                    if (param == null)
                    {
                        data = await db.ExecuteScalarAsync<string>(Query, commandType: commandType);
                    }
                    else
                    {
                        data = await db.ExecuteScalarAsync<string>(Query, param, commandType: commandType);
                    }
                }
                catch (Exception)
                {
                }
            }
            return data;
        }

        public async Task<string> ExecuteScalarAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            string DBQuery = this._query.GetDBQuery(foldername, queryID, param);
            string data = string.Empty;
            if (!string.IsNullOrWhiteSpace(DBQuery))
            {
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    try
                    {
                        if (param == null)
                        {
                            data = await db.ExecuteScalarAsync<string>(DBQuery, commandType: commandType);
                        }
                        else
                        {
                            data = await db.ExecuteScalarAsync<string>(DBQuery, param, commandType: commandType);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return data;
        }

        public async Task<List<T>> GetDataAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            string DBQuery = this._query.GetDBQuery(foldername, queryID, param);

            if (!string.IsNullOrWhiteSpace(DBQuery))
            {
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    try
                    {
                        if (param == null)
                        {
                            IEnumerable<T> data = await db.QueryAsync<T>(DBQuery, commandType: commandType);
                            return data.ToList();
                        }
                        else
                        {
                            IEnumerable<T> data = await db.QueryAsync<T>(DBQuery, param, commandType: commandType);
                            return data.ToList();
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public async Task<List<T>> GetDataAsync<T>(string Query, DynamicParameters param, CommandType commandType)
        {
            using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                try
                {
                    if (param == null)
                    {
                        IEnumerable<T> data = await db.QueryAsync<T>(Query, commandType: commandType);
                        return data.ToList();
                    }
                    else
                    {
                        IEnumerable<T> data = await db.QueryAsync<T>(Query, param, commandType: commandType);
                        return data.ToList();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<DataSet> GetDataSetAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            string DBQuery = this._query.GetDBQuery(foldername, queryID, param);
            if (!string.IsNullOrWhiteSpace(DBQuery))
            {
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    try
                    {
                        if (param == null)
                        {
                            IDataReader list = await db.ExecuteReaderAsync(DBQuery, commandType: commandType);
                            DataSet dataset = this.ConvertDataReaderToDataSet(list);
                            return dataset;
                        }
                        else
                        {
                            IDataReader list = await db.ExecuteReaderAsync(DBQuery, param, commandType: commandType);
                            DataSet dataset = this.ConvertDataReaderToDataSet(list);
                            return dataset;
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public async Task<DataSet> GetDataSetAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                try
                {
                    if (param == null)
                    {
                        IDataReader list = await db.ExecuteReaderAsync(Query, commandType: commandType);
                        DataSet dataset = this.ConvertDataReaderToDataSet(list);
                        return dataset;
                    }
                    else
                    {
                        IDataReader list = await db.ExecuteReaderAsync(Query, param, commandType: commandType);
                        DataSet dataset = this.ConvertDataReaderToDataSet(list);
                        return dataset;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public DataSet ConvertDataReaderToDataSet(IDataReader data)
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