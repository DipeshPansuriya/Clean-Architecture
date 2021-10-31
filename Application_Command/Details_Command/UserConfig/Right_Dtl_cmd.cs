using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Details_Command.UserConfig
{
    public class Right_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Right_Dtl_cmd_Handeler : IRequestHandler<Right_Dtl_cmd, Response>
        {
            private readonly IDapper<TblRightmaster> _rights;

            public Right_Dtl_cmd_Handeler(IDapper<TblRightmaster> rights)
            {
                _rights = rights;
            }

            public async Task<Response> Handle(Right_Dtl_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRightmaster entity = await _rights.GetDetails(request.Id);

                    if (entity != null)
                    {
                        response.ResponseObject = entity;
                    }
                }
                catch (Exception ex)
                {
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return response;
            }
        }
    }
}