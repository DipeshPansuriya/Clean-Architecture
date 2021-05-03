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

        protected IMediator Mediator => this._mediator ?? (this._mediator = this.HttpContext.RequestServices.GetService<IMediator>());

        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}