using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminUser.InsertUpdate
{
    public class Adm_User_InstUpd : IRequest<Response>
    {
        public int UserId { get; set; }
        public int OrgProdId { get; set; }
        public string LoginMail { get; set; }
        public string LoginPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int CMUserId { get; set; }

        public class Adm_User_InstUpdHandler : IRequestHandler<Adm_User_InstUpd, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public Adm_User_InstUpdHandler(INotificationMsg notification,
                IDapper<Response> dapper
                )
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Adm_User_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@UserId", request.UserId);
                    param.Add("@OrgProdId", request.OrgProdId);
                    param.Add("@LoginMail", request.LoginMail);
                    param.Add("@LoginPassword", request.LoginPassword);
                    param.Add("@FirstName", request.FirstName);
                    param.Add("@LastName", request.LastName);
                    param.Add("@IsActive", request.IsActive);
                    param.Add("@UserId", request.UserId);

                    response.ResponseObject = await dapper.ExecuteScalarAsync("sp_AdminUser_InsertUpdat", param, System.Data.CommandType.StoredProcedure);
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