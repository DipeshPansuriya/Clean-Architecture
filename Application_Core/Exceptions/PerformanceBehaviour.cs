using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Core.Exceptions
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            this._timer = new Stopwatch();

            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this._timer.Start();

            TResponse response = await next();

            this._timer.Stop();

            if (this._timer.ElapsedMilliseconds > 500)
            {
                string name = typeof(TRequest).Name;

                this._logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    name, this._timer.ElapsedMilliseconds, 0, request);

                //nlogcls.logperformance(name, Convert.ToString(_timer.ElapsedMilliseconds), "0", JsonConvert.SerializeObject(request));
            }

            return response;
        }
    }
}