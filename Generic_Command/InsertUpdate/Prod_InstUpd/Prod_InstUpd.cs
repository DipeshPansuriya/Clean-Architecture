using Application_Common;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace Generic_Command.InsertUpdate.Prod_InstUpd
{
    public class Prod_InstUpd : IRequest<Response>
    {
        public int ProdId { get; set; }
        public string ProductName { get; set; }
        public bool IsActive { get; set; }

        public class Prod_InstUpdHandler : IRequestHandler<Prod_InstUpd, Response>
        {
            private readonly IDapper<Response> dapper;

            public Prod_InstUpdHandler(IDapper<Response> dapper)
            {
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Prod_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    List<Prod_InstUpd> prods = new()
                    {
                        new Prod_InstUpd { ProdId = 1,ProductName = "Core", IsActive = true},
                        new Prod_InstUpd { ProdId = 2,ProductName = "Logistics", IsActive = true},
                        new Prod_InstUpd { ProdId = 3,ProductName = "Warehouse", IsActive = true},
                        new Prod_InstUpd { ProdId = 4,ProductName = "Accounts", IsActive = true},
                    };
                    string XMLproduct = GenericFunction.ClassToXML<List<Prod_InstUpd>>(prods);
                    DynamicParameters param = new();

                    param.Add("@ProductXML", XMLproduct);

                    string? data = await dapper.ExecuteScalarAsync
                       ("sp_AdminProduct_InsertUpdate",
                       param,
                       System.Data.CommandType.StoredProcedure
                       );

                    response.ResponseStatus = true;
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