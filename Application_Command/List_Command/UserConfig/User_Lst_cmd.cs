﻿using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.List_Command.UserConfig
{
    public class User_Lst_cmd : IRequest<Response>
    {
        public class User_Lst_cmd_Handeler : IRequestHandler<User_Lst_cmd, Response>
        {
            private readonly IDapper _dapper;
            private readonly ICacheService _cache;
            private readonly IBackgroundJob _backgroundJob;

            public User_Lst_cmd_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob)
            {
                _dapper = dapper;
                _cache = cache;
                _backgroundJob = backgroundJob;
            }

            public async Task<Response> Handle(User_Lst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    bool cachexists = false;
                    Task<List<TblUsermaster>> data = _cache.GetCachedObject<TblUsermaster>("users");

                    cachexists = data == null ? true : data.Result != null ? true : false;
                    if (cachexists)
                    {
                        response.ResponseObject = data.Result;
                    }
                    else
                    {
                        List<TblUsermaster> dbdata = await _dapper.GetDataAsync<TblUsermaster>("users", "1", null, CommandType.Text);
                        if (dbdata != null)
                        {
                            Parallel.Invoke(() => _backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("users", dbdata)));

                            response.ResponseObject = dbdata;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message +" ~ "+ ex.InnerException;
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return response;
            }
        }
    }
}