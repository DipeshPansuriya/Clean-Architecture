using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Genric;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public class Dapper<T> : IDapper<T> where T : class
    {
        private readonly IConfiguration _config;
        private readonly string msg = string.Empty;
        private readonly ILogger<Dapper<T>> _logger;
        private readonly IGetQuery _query;
        private readonly APP_DbContext _dbContext;
        private readonly IBackgroundJob _backgroundJob;
        private readonly ICacheService _cache;

        public Dapper(IConfiguration config, ILogger<Dapper<T>> logger, IGetQuery query, APP_DbContext dbContext, IBackgroundJob backgroundClient, ICacheService cache)
        {
            _config = config;
            _logger = logger;
            _query = query;
            _dbContext = dbContext;
            _backgroundJob = backgroundClient;
            _cache = cache;
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
                if (!string.IsNullOrWhiteSpace(DBQuery))
                    return await ExecuteAsync(DBQuery, null, commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
            return -1;
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
                if (!string.IsNullOrWhiteSpace(DBQuery))
                    return await ExecuteScalarAsync(DBQuery, null, commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T>(string Query, DynamicParameters param, CommandType commandType)
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

        public async Task<IEnumerable<T>> GetDataAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                if (!string.IsNullOrWhiteSpace(DBQuery))
                    return await GetDataAsync<T>(DBQuery, null, commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
            return null;
        }

        public async Task<T> GetDataFirstorDefaultAsync<T>(string Query, DynamicParameters param, CommandType commandType)
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

                    return data.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<T> GetDataFirstorDefaultAsync<T>(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                if (!string.IsNullOrWhiteSpace(DBQuery))
                    return await GetDataFirstorDefaultAsync<T>(DBQuery, null, commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
            return default(T);
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

        public async Task<DataSet> GetDataSetAsync(string foldername, string queryID, DynamicParameters param, CommandType commandType)
        {
            try
            {
                string DBQuery = _query.GetDBQuery(foldername, queryID, param);
                if (!string.IsNullOrWhiteSpace(DBQuery))
                    return await GetDataSetAsync(DBQuery, null, commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
            return null;
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

        public async Task<int> SaveAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey)));
                }
            }

            return await SaveAsync(entity);
        }

        public async Task<int> SaveAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<int> SaveNotificationAsync(NotficationCls entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            TblNotification tblNotification = new()
            {
                MsgFrom = entity.MsgFrom,
                MsgTo = entity.MsgTo,
                MsgSubject = entity.MsgSubject,
                MsgBody = entity.MsgBody,
                MsgSatus = entity.MsgSatus.ToString(),
                MsgType = entity.MsgType.ToString(),
            };

            try
            {
                await _dbContext.Set<TblNotification>().AddAsync(tblNotification);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task DeleteAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey)));
                }
            }
            await DeleteAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                _dbContext.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<int> UpdateAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey)));
                }
            }
            return await UpdateAsync(entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;

                //_dbContext.Set<T>().Update(entity);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<int> UpdateNotificationAsync(NotficationCls entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                TblNotification tblNotification = new()
                {
                    Id = entity.Id,
                    MsgType = entity.MsgType.ToString(),
                    MsgFrom = entity.MsgFrom,
                    MsgTo = entity.MsgTo,
                    MsgCc = entity.MsgCC,
                    MsgSubject = entity.MsgSubject,
                    MsgBody = entity.MsgBody,
                    MsgSatus = entity.MsgSatus.ToString(),
                    FailDetails = entity.FailDetails,
                };

                _dbContext.Entry(tblNotification).State = EntityState.Modified;

                //_dbContext.Set<T>().Update(entity);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }

        public async Task<T> GetDetails(int id)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ~ " + ex.InnerException);
                throw;
            }
        }
    }
}