﻿using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;
using User_Command.AdminBranch.List;

namespace User_Command.AdminRole.Select
{
    public class Adm_Role_Select : IRequest<Response>
    {
        public int RoleId { get; set; }
        public int OrgProdId { get; set; }

        public class Adm_Role_SelectHandler : IRequestHandler<Adm_Role_Select, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public Adm_Role_SelectHandler(INotificationMsg notification,
                IDapper<Response> dapper
                )
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Adm_Role_Select request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();
                    param.Add("@RoleId", request.RoleId);
                    param.Add("@OrgProdId", request.OrgProdId);

                    Adm_Role_Lst_DTO? data = (await dapper.GetDataListAsync<Adm_Role_Lst_DTO>("sp_AdminRole_Select", param, System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    if (data != null)
                    {
                        response.ResponseObject = data;
                    };
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                }
                return response;
            }
        }
    }
}