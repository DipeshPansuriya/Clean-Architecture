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
    public class Role_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Role_Dtl_cmd_Handeler : IRequestHandler<Role_Dtl_cmd, Response>
        {
            private readonly IDapper<TblRolemaster> _roles;

            public Role_Dtl_cmd_Handeler(IDapper<TblRolemaster> roles)
            {
                _roles = roles;
            }

            public async Task<Response> Handle(Role_Dtl_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRolemaster entity = await _roles.GetDetails(request.Id);

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