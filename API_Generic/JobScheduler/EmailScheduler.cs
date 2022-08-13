using Application_Common;
using Generic_Command.List;
using MediatR;

namespace API_Generic.JobScheduler
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