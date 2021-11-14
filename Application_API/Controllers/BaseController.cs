using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController<T> : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<T> _loggerInstance;
        //private IResponse_Request _response_request;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        //protected IResponse_Request RequestResponse => _response_request ?? (_response_request = HttpContext.RequestServices.GetService<IResponse_Request>());
    }
}