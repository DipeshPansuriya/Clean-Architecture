using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using HealthChecks.UI.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command.UserConfig
{
    public class Role_Lst_cmd : IRequest<Response>
    {

        public class Role_Lst_cmd_Handeler : IRequestHandler<Role_Lst_cmd, Response>
        {
            private readonly IDapper<TblRolemaster> _dapper;
            private readonly ICacheService _cache;
            private readonly IBackgroundJob _backgroundJob;

            public Role_Lst_cmd_Handeler(IDapper<TblRolemaster> dapper, ICacheService cache, IBackgroundJob backgroundJob)
            {
                this._dapper = dapper;
                this._cache = cache;
                this._backgroundJob = backgroundJob;
            }

            public async Task<Response> Handle(Role_Lst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {

                    bool cachexists = false;
                    Task<List<TblRolemaster>> data = _cache.GetCachedObject<TblRolemaster>("roles");
                    cachexists = data == null ? true : data.Result != null ? true : false;
                    if (cachexists)
                    {
                        response.ResponseObject = data.Result;
                    }
                    else
                    {
                        List<TblRolemaster> dbdata = (List<TblRolemaster>)await _dapper.GetDataAsync<TblRolemaster>("users", "2", null, CommandType.Text);
                        if (dbdata != null)
                        {
                            Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("roles", dbdata)));

                            response.ResponseObject = dbdata;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return response;
            }
        }
    }
}