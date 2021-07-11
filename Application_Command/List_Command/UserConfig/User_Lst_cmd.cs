using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command.UserConfig
{
    public class User_Lst_cmd : IRequest<Response>
    {
    }

    public class User_Lst_cmd_Handeler : IRequestHandler<User_Lst_cmd, Response>
    {
        private readonly IDapper _demoCustomer;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;

        public User_Lst_cmd_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob)
        {
            this._demoCustomer = dapper;
            this._cache = cache;
            this._backgroundJob = backgroundJob;
        }

        public async Task<Response> Handle(User_Lst_cmd request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            bool cachexists = false;
            Task<List<Demo_Customer>> data = this._cache.GetCachedObject<Demo_Customer>("users");
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
                List<Demo_Customer> dbdata = await this._demoCustomer.GetDataAsync<Demo_Customer>("users", "1", null, CommandType.Text);
                this._backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("users", dbdata));

                response.ResponseObject = dbdata;
            }
            response.ResponseStatus = "success";

            return response;
        }
    }
}