using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminComp.InsertUpdate
{
    public class Adm_Comp_InstUpd : IRequest<Response>
    {
        public int CompId { get; set; }
        public int OrgProdId { get; set; }
        public string CompName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string TelephoneNo { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public class Adm_Comp_InstUpdHandler : IRequestHandler<Adm_Comp_InstUpd, Response>
        {
            private readonly INotificationMsg notificationMsg;
            private readonly IDapper<Response> aPPDbContext;

            public Adm_Comp_InstUpdHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                this.notificationMsg = notificationMsg;
                this.aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Comp_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@CompId", request.CompId);
                    param.Add("@OrgProdId", request.OrgProdId);
                    param.Add("@CompName", request.CompName);
                    param.Add("@Address1", request.Address1);
                    param.Add("@Address2", request.Address2);
                    param.Add("@Address3", request.Address3);
                    param.Add("@TelephoneNo", request.TelephoneNo);
                    param.Add("@IsActive", request.IsActive);
                    param.Add("@UserId", request.UserId);

                    response.ResponseObject = await aPPDbContext.ExecuteScalarAsync("sp_AdminCompany_InsertUpdate", param, System.Data.CommandType.StoredProcedure);
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