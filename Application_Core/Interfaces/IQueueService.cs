using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Core.Interfaces
{
    public interface IQueueService
    {
        void QueueTask(Func<CancellationToken, Task> task);

        Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken);
    }
}