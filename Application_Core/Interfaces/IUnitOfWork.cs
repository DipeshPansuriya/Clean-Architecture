using Application_Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<Response> Commit(CancellationToken cancellationToken);
    }
}