using Application_Command.List_Command;
using Application_Genric;
using MediatR;
using System.Threading.Tasks;

namespace Application_API.JobScheduler
{
    public class EmailScheduler
    {
        private readonly IMediator _mediator;

        public EmailScheduler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Task> SendPendingMail()
        {
            Response res = await _mediator.Send(new List_SendEmail());

            return Task.CompletedTask;
        }
    }
}