using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command
{
    public class Demo_Customer_lst_cmd : IRequest<Response>
    {
    }

    public class Demo_Customer_lst_Handeler : IRequestHandler<Demo_Customer_lst_cmd, Response>
    {
        private readonly IDapper _demoCustomer;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IGetQuery _getQuery;

        public Demo_Customer_lst_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob, IGetQuery getQuery)
        {
            this._demoCustomer = dapper;
            this._cache = cache;
            this._backgroundJob = backgroundJob;
            this._getQuery = getQuery;
        }

        public async Task<Response> Handle(Demo_Customer_lst_cmd request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            bool cachexists = false;
            Task<System.Collections.Generic.List<Demo_Customer>> data = this._cache.GetCachedObject<Demo_Customer>("democust");
            if (data != null)
            {
                if (data.Result != null)
                {
                    response.ResponseObject = data.Result;
                    cachexists = true;
                }
            }

            if (cachexists == false)
            {
                string Query = this._getQuery.GetDBQuery("Demo_Cust", "1");
                System.Collections.Generic.List<Demo_Customer> dbdata = await this._demoCustomer.GetAll<Demo_Customer>(Query, null, System.Data.CommandType.Text);
                this._backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("democust", dbdata));

                response.ResponseObject = dbdata;
            }
            response.ResponseStatus = "success";

            return response;
        }
    }
}