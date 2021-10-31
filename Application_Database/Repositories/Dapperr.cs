﻿using Application_Core.Repositories;
using Application_Genric;
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
            _config = config;
            _logger = logger;
            _query = query;
        }

        public void Dispose()
        {
        }

        public async Task<int> ExecuteAsync(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                int affectedRows = 0;

                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

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

        public async Task<int> ExecuteAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                int affectedRows = 0;
                if (!string.IsNullOrWhiteSpace(DBQuery))
                {
                    using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                        }

                        affectedRows = param == null ? await db.ExecuteAsync(DBQuery, commandType: commandType) : await db.ExecuteAsync(DBQuery, param, commandType: commandType);
                    }
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

                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

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

        public async Task<string> ExecuteScalarAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                string data = string.Empty;
                if (!string.IsNullOrWhiteSpace(DBQuery))
                {
                    using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                        }

                        data = param == null ? await db.ExecuteScalarAsync<string>(DBQuery, commandType: commandType) : await db.ExecuteScalarAsync<string>(DBQuery, param, commandType: commandType);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<List<T>> GetDataAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);

                if (!string.IsNullOrWhiteSpace(DBQuery))
                {
                    using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                        }

                        IEnumerable<T> data = param == null ? await db.QueryAsync<T>(DBQuery, commandType: commandType) : await db.QueryAsync<T>(DBQuery, param, commandType: commandType);

                        return data.ToList();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<List<T>> GetDataAsync<T>(string Query, DynamicParameters param, CommandType commandType)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

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

        public async Task<DataSet> GetDataSetAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                if (!string.IsNullOrWhiteSpace(DBQuery))
                {
                    using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                        }

                        IDataReader list = param == null ? await db.ExecuteReaderAsync(DBQuery, commandType: commandType) : await db.ExecuteReaderAsync(DBQuery, param, commandType: commandType);

                        DataSet dataset = ConvertDataReaderToDataSet(list);
                        return dataset;
                    }
                }
                return null;
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
                using (IDbConnection db = new SqlConnection(APISetting.DBConnection))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

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