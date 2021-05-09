using Application_Command.List_Command;
using MediatR;
using System.Threading.Tasks;

namespace Application_API.JobScheduler
{
    public class EmailScheduler
    {
        private IMediator _mediator;

        public EmailScheduler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Task> SendPendingMail()
        {
            var res = await this._mediator.Send(new List_SendEmail());

            return Task.CompletedTask;
        }
    }
}