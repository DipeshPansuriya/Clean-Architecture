using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command
{
    public class List_SendEmail : IRequest<Response>
    {
    }

    public class List_SendEmail_Handeler : IRequestHandler<List_SendEmail, Response>
    {
        private readonly IDapper _query;
        private readonly ICacheService _cache;
        private IBackgroundJob _backgroundJob;
        private readonly IGetQuery _getQuery;
        private INotificationMsg _notification;

        public List_SendEmail_Handeler(IDapper demoCustomer, ICacheService cache, IBackgroundJob backgroundJob, IGetQuery getQuery, INotificationMsg notification)
        {
            this._query = demoCustomer;
            this._cache = cache;
            this._backgroundJob = backgroundJob;
            this._getQuery = getQuery;
            this._notification = notification;
        }

        public async Task<Response> Handle(List_SendEmail request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            string Query = this._getQuery.GetDBQuery("JobScheduler", "1");
            var dbdata = await this._query.GetAll<NotficationCls>(Query, null, System.Data.CommandType.Text);

            foreach (var data in dbdata)
            {
                var jobid = _backgroundJob.AddEnque<INotificationMsg>(x => x.Send(data));
            }

            //response.ResponseObject = dbdata;

            return response;
        }
    }
}