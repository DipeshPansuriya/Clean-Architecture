using Application_Core.Exceptions;
using Application_Core.Interfaces;
using Application_Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly APP_DbContext _dbContext;

        private ILogger<UnitOfWork> _logger;
        private bool disposed;

        public UnitOfWork(APP_DbContext dbContext, ILogger<UnitOfWork> logger)//, IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
            //_authenticatedUserService = authenticatedUserService;
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
                msg = "Application Error :- ";
                msg = msg + ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    msg = msg + " ~ InnerException ~ " + ex.InnerException.Message.ToString();
                }
                _logger.LogError(ex, msg);
                throw new AppException(msg);
            }

            Response response = new Response()
            {
                ResponseMessage = msg,
                ResponseStatus = (msg == string.Empty ? "success" : "fail")
            };
            return response;
        }

        public Task Rollback()
        {
            //todo
            return Task.CompletedTask;
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