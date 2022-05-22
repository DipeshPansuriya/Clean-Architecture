using Application_Common;
using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Notification;
using MediatR;

namespace Generic_Command.List_Command
{
    public class List_SendEmail : IRequest<Response>
    {
        public class List_SendEmail_Handeler : IRequestHandler<List_SendEmail, Response>
        {
            private readonly ICacheService _cache;
            private readonly IBackgroundJob _backgroundJob;
            private readonly INotificationMsg _notification;

            public List_SendEmail_Handeler(ICacheService cache, IBackgroundJob backgroundJob, INotificationMsg notification)
            {
                _cache = cache;
                _backgroundJob = backgroundJob;
                _notification = notification;
            }

            public async Task<Response> Handle(List_SendEmail request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                List<NotficationCls> dbdata = null;
                //List<NotficationCls> dbdata = (List<NotficationCls>)await _query.GetDataAsync<NotficationCls>("JobScheduler", "1", null, CommandType.Text);

                Parallel.ForEach(dbdata, data =>
                {
                    string jobid = _backgroundJob.AddEnque<INotificationMsg>(x => x.SendAsync(data));
                });

                return response;
            }
        }
    }
}