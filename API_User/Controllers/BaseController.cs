using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Users.Controllers
{
    [ApiController]
    [Authorize]
    [Route("API_Users/[controller]/[action]")]
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