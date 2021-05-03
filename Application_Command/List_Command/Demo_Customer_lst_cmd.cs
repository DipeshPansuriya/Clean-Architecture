﻿using Application_Core.Interfaces;
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
        private IGetQuery _getQuery;

        public Demo_Customer_lst_Handeler(IDapper dapper, ICacheService cache, IBackgroundJob backgroundJob, IGetQuery getQuery)
        {
            _demoCustomer = dapper;
            _cache = cache;
            _backgroundJob = backgroundJob;
            _getQuery = getQuery;
        }

        public async Task<Response> Handle(Demo_Customer_lst_cmd request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            bool catchexists = false;
            var data = _cache.GetCachedObject<Demo_Customer>("democust");
            if (data != null)
            {
                if (data.Result != null)
                {
                    response.ResponseObject = data.Result;
                    catchexists = true;
                }
            }

            if (catchexists == false)
            {
                string Query = _getQuery.GetDBQuery("Demo_Cust", "1");
                var dbdata = await this._demoCustomer.GetAll<Demo_Customer>(Query, null, System.Data.CommandType.Text);
                _backgroundJob.AddEnque<ICacheService>(x => x.SetCachedObject("democust", dbdata));

                response.ResponseObject = dbdata;
            }
            response.ResponseStatus = "success";

            return response;
        }
    }
}