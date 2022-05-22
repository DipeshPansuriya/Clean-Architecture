using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminRole.InsertUpdate
{
    public class Adm_Role_InstUpd : IRequest<Response>
    {
        public int RoleId { get; set; }
        public int OrgProdId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public class Adm_Role_InstUpdHandler : IRequestHandler<Adm_Role_InstUpd, Response>
        {
            private readonly INotificationMsg notificationMsg;
            private readonly IDapper<Response> aPPDbContext;

            public Adm_Role_InstUpdHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                this.notificationMsg = notificationMsg;
                this.aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Role_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@RoleId", request.RoleId);
                    param.Add("@OrgProdId", request.OrgProdId);
                    param.Add("@RoleName", request.RoleName);
                    param.Add("@IsActive", request.IsActive);
                    param.Add("@UserId", request.UserId);

                    response.ResponseObject = await aPPDbContext.ExecuteScalarAsync("sp_AdminRole_InsertUpdate", param, System.Data.CommandType.StoredProcedure);
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