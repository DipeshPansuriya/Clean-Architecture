﻿namespace Application_Core.Exceptions
{
    //public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly Stopwatch _timer;
    //    private readonly ILogger<TRequest> _logger;

    //    public PerformanceBehaviour(ILogger<TRequest> logger)
    //    {
    //        _timer = new Stopwatch();

    //        _logger = logger;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        _timer.Start();

    //        TResponse response = await next();

    //        _timer.Stop();

    //        if (_timer.ElapsedMilliseconds > 500)
    //        {
    //            string name = typeof(TRequest).Name;

    //            _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
    //                name, _timer.ElapsedMilliseconds, 0, request);

    //            //nlogcls.logperformance(name, Convert.ToString(_timer.ElapsedMilliseconds), "0", JsonConvert.SerializeObject(request));
    //        }

    //        return response;
    //    }
    //}
}