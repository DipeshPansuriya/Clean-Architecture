using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Genric;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly APP_DbContext _dbContext;
        private readonly IBackgroundJob _backgroundJob;
        private readonly ICacheService _cache;
        private readonly ILogger<RepositoryAsync<T>> _logger;

        public RepositoryAsync(APP_DbContext dbContext, IBackgroundJob backgroundClient, ICacheService cache, ILogger<RepositoryAsync<T>> logger)
        {
            _dbContext = dbContext;
            _backgroundJob = backgroundClient;
            _cache = cache;
            _logger = logger;
        }

        //public IQueryable<T> Entities => _dbContext.Set<T>();

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