﻿using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using MediatR;
using System.Net;

namespace User_Command.AdminRole.InsertUpdate
{
    public class Adm_Role_InstUpd : IRequest<Response>
    {
        public class Adm_Role_InstUpdHandler : IRequestHandler<Adm_Role_InstUpd, Response>
        {
            private readonly INotificationMsg _notificationMsg;
            private readonly IDapper<Response> _aPPDbContext;

            public Adm_Role_InstUpdHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                _notificationMsg = notificationMsg;
                _aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Role_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    response.ResponseObject = "";
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