using Application_Core.Interfaces;
using Application_Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APP_DbContext _dbContext;

        private bool disposed;

        public UnitOfWork(APP_DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Response> Commit(CancellationToken cancellationToken)
        {
            string msg = string.Empty;
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw new ArgumentException($"Error in UnitOfWork.CS :- " + ex.Message, ex.InnerException);
            }

            Response response = new Response()
            {
                ResponseMessage = msg,
                ResponseStatus = "success"
            };
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}