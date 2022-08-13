using Application_Common;
using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Notification;
using MediatR;

namespace Generic_Command.List
{
    public class List_SendEmail : IRequest<Response>
    {
        public class List_SendEmail_Handeler : IRequestHandler<List_SendEmail, Response>
        {
            private readonly ICacheService cache;
            private readonly IBackgroundJob backgroundJob;
            private readonly INotificationMsg notification;

            public List_SendEmail_Handeler(ICacheService cache, IBackgroundJob backgroundJob, INotificationMsg notification)
            {
                this.cache = cache;
                this.backgroundJob = backgroundJob;
                this.notification = notification;
            }

            public async Task<Response> Handle(List_SendEmail request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                List<NotficationCls> dbdata = null;
                //List<NotficationCls> dbdata = (List<NotficationCls>)await _query.GetDataAsync<NotficationCls>("JobScheduler", "1", null, CommandType.Text);

                Parallel.ForEach(dbdata, data =>
                {
                    string jobid = backgroundJob.AddEnque<INotificationMsg>(x => x.SendAsync(data));
                });

                return response;
            }
        }
    }
}