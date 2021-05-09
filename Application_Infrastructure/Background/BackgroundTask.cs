using Application_Core.Background;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Infrastructure.Background
{
    public class BackgroundTask : BackgroundService
    {
        private IQueueService _queue;
        private readonly IServiceProvider _service;
        public static IServiceProvider serviceCollection { get; set; }

        public BackgroundTask(IQueueService queue, IServiceProvider service)
        {
            _queue = queue;
            _service = service;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            serviceCollection = this._service;
            while (stoppingToken.IsCancellationRequested == false)
            {
                var task = await _queue.PopQueue(stoppingToken);
                await task(stoppingToken);
            }
        }
    }
}