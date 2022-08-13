using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminOrg.InsertUpdate
{
    public class OrgProdcut
    {
        public int ProductId { get; set; }
    }

    public class Adm_Org_InstUpd : IRequest<Response>
    {
        public int Id { get; set; } = 0;
        public string OrgName { get; set; }
        public string OrgEmail { get; set; }
        public bool IsActive { get; set; } = true;
        public List<OrgProdcut> OrgProdcut { get; set; }
        public bool IsFreshSetup { get; set; }

        public class Adm_Org_InstHandler : IRequestHandler<Adm_Org_InstUpd, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public Adm_Org_InstHandler(INotificationMsg notification,
                IDapper<Response> dapper
                )
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Adm_Org_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    string XMLproduct = GenericFunction.ClassToXML<List<OrgProdcut>>(request.OrgProdcut);
                    DynamicParameters param = new();

                    param.Add("@Id", request.Id);
                    param.Add("@OrgName", request.OrgName);
                    param.Add("@OrgEmail", request.OrgEmail);
                    param.Add("@Pwd", "123");
                    param.Add("@IsActive", request.IsActive);
                    param.Add("@ProductXML", XMLproduct);
                    param.Add("@IsFreshSetup", request.IsFreshSetup);

                    string? data = await dapper.ExecuteScalarAsync
                        ("sp_AdminOrganization_InsertUpdate",
                        param,
                        System.Data.CommandType.StoredProcedure
                        );

                    response.ResponseStatus = true;
                    response.ResponseObject = data;
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