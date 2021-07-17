using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command.UserConfig
{
    public class Role_Lst_cmd : IRequest<Response>
    {
    }

    public class Role_Lst_cmd_Handeler : IRequestHandler<Role_Lst_cmd, Response>
    {
        private readonly IDapper _dapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;

        public Role_Lst_cmd_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob)
        {
            _dapper = dapper;
            _cache = cache;
            _backgroundJob = backgroundJob;
        }

        public async Task<Response> Handle(Role_Lst_cmd request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            bool cachexists = false;
            Task<List<role_cls>> data = _cache.GetCachedObject<role_cls>("roles");
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
                List<role_cls> dbdata = await _dapper.GetDataAsync<role_cls>("users", "2", null, CommandType.Text);
                _backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("roles", dbdata));

                response.ResponseObject = dbdata;
            }
            response.ResponseStatus = "success";

            return response;
        }
    }
}