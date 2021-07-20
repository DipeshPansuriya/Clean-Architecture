using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using Microsoft.EntityFrameworkCore;
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
                    _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey));
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
            //return entity;
        }

        public async Task<Response> DeleteAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey));
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

            _dbContext.Set<T>().Remove(entity);
            Response response = new Response()
            {
                ResponseMessage = "Success",
                ResponseStatus = "Success",
                ResponseObject = entity
            };
            return response;
        }

        public async Task<Response> UpdateAsync(T entity, bool IsCache, string Cahekey)
        {
            if (IsCache)
            {
                if (!string.IsNullOrEmpty(Cahekey))
                {
                    _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache(Cahekey));
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