using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminBranch.InsertUpdate
{
    public class Adm_Bran_InstUpd : IRequest<Response>
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int OrgProdId { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public bool IsHo { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public class Adm_Bran_InstUpdHandler : IRequestHandler<Adm_Bran_InstUpd, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public Adm_Bran_InstUpdHandler(INotificationMsg notification,
                IDapper<Response> dapper
                )
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Adm_Bran_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@BranchId", request.BranchId);
                    param.Add("@CompId", request.CompId);
                    param.Add("@OrgProdId", request.OrgProdId);
                    param.Add("@BranchName", request.BranchName);
                    param.Add("@Address1", request.Address1);
                    param.Add("@Address2", request.Address2);
                    param.Add("@Address3", request.Address3);
                    param.Add("@IsHo", request.IsHo);
                    param.Add("@IsActive", request.IsActive);
                    param.Add("@UserId", request.UserId);

                    response.ResponseObject = await dapper.ExecuteScalarAsync("sp_AdminBranch_InsertUpdate", param, System.Data.CommandType.StoredProcedure);
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