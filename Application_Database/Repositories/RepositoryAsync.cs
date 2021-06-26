using Application_Core.Repositories;
using Application_Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly APP_DbContext _dbContext;

        public RepositoryAsync(APP_DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<Response> AddAsync(T entity)
        {
            if (this._dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                this._dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            await this._dbContext.Set<T>().AddAsync(entity);
            int resulst = await this._dbContext.SaveChangesAsync();
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

        public async Task<Response> DeleteAsync(T entity)
        {
            if (this._dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                this._dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            this._dbContext.Set<T>().Remove(entity);
            Response response = new Response()
            {
                ResponseMessage = "Success",
                ResponseStatus = "Success",
                ResponseObject = entity
            };
            return response;
        }

        public async Task<Response> UpdateAsync(T entity)
        {
            if (this._dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                this._dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            this._dbContext.Entry(entity).State = EntityState.Modified;

            //_dbContext.Set<T>().Update(entity);
            int resulst = await this._dbContext.SaveChangesAsync();
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
            if (this._dbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                this._dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            return await this._dbContext.Set<T>().FindAsync(id);
        }
    }
}