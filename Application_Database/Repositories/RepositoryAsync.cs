using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly APP_DbContext _dbContext;
        private readonly IBackgroundJob _backgroundJob;
        private readonly ICacheService _cache;

        public RepositoryAsync(APP_DbContext dbContext, IBackgroundJob backgroundClient, ICacheService cache)
        {
            _dbContext = dbContext;
            _backgroundJob = backgroundClient;
            _cache = cache;
        }

        //public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<Response> SaveAsync(T entity, bool IsCache, string Cahekey)
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

        public async Task<Response> SaveAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                int resulst = await _dbContext.SaveChangesAsync();
                Response response = new Response()
                {
                    ResponseId = resulst,
                    ResponseMessage = "Success",
                    ResponseStatus = "Success",
                    ResponseObject = entity
                };

                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response()
                {
                    ResponseId = 0,
                    ResponseMessage = ex.Message + " ~ " + ex.InnerException,
                    ResponseStatus = "Failure",
                    ResponseObject = entity,
                    StatusCode = HttpStatusCode.BadRequest,
                };


                return response;
            }
        }

        public async Task<Response> DeleteAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey)));
                }
            }
            return await DeleteAsync(entity);
        }

        public async Task<Response> DeleteAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                _dbContext.Set<T>().Remove(entity);
                Response response = new Response()
                {
                    ResponseMessage = "Success",
                    ResponseStatus = "Success",
                    ResponseObject = entity
                };
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response()
                {
                    ResponseId = 0,
                    ResponseMessage = ex.Message + "~" + ex.InnerException,
                    ResponseStatus = "Failure",
                    ResponseObject = entity,
                    StatusCode = HttpStatusCode.BadRequest,
                };

                return response;
            }
        }

        public async Task<Response> UpdateAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey)));
                    //_backgroundJob.AddSchedule<ICacheService>(x => x.RemoveCache("democust"), RecuringTime.Seconds, 2);
                }
            }
            return await UpdateAsync(entity);
        }

        public async Task<Response> UpdateAsync(T entity)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;

                //_dbContext.Set<T>().Update(entity);
                int resulst = await _dbContext.SaveChangesAsync();
                Response response = new Response()
                {
                    ResponseId = resulst,
                    ResponseMessage = "Success",
                    ResponseStatus = "Success",
                    ResponseObject = entity
                };
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response()
                {
                    ResponseId = 0,
                    ResponseMessage = ex.Message + "~" + ex.InnerException,
                    ResponseStatus = "Failure",
                    ResponseObject = entity,
                    StatusCode = HttpStatusCode.BadRequest,
                };

                return response;
            }
        }

        public async Task<T> GetDetails(int id)
        {
            if (_dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}