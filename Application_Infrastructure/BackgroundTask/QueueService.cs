using Application_Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Infrastructure.BackgroundTask
{
    public class QueueService : IQueueService
    {
        public ConcurrentQueue<Func<CancellationToken, Task>> Tasks;
        private SemaphoreSlim Signal;

        public QueueService()
        {
            Tasks = new ConcurrentQueue<Func<CancellationToken, Task>>();
            Signal = new SemaphoreSlim(0);
        }

        void IQueueService.QueueTask(Func<CancellationToken, Task> task)
        {
            Tasks.Enqueue(task);

            Signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken)
        {
            await Signal.WaitAsync(cancellationToken);
            Tasks.TryDequeue(out var task);
            return task;
        }
    }
}