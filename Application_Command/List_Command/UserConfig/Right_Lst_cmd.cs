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
    public class Right_Lst_cmd : IRequest<Response>
    {

        public class Right_Lst_cmd_Handeler : IRequestHandler<Right_Lst_cmd, Response>
        {
            private readonly IDapper _dapper;
            private readonly ICacheService _cache;
            private readonly IBackgroundJob _backgroundJob;

            public Right_Lst_cmd_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob)
            {
                _dapper = dapper;
                _cache = cache;
                _backgroundJob = backgroundJob;
            }

            public async Task<Response> Handle(Right_Lst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                bool cachexists = false;
                Task<List<rights_cls>> data = _cache.GetCachedObject<rights_cls>("rights");
                cachexists = data != null ? true : data.Result != null ? true : false;
                if (cachexists)
                {
                    response.ResponseObject = data.Result;
                }
                else
                {
                    List<rights_cls> dbdata = await _dapper.GetDataAsync<rights_cls>("users", "3", null, CommandType.Text);
                    _backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("rights", dbdata));

                    response.ResponseObject = dbdata;
                }
                response.ResponseStatus = "success";

                return response;
            }
        }
    }
}