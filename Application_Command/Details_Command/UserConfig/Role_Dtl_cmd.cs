using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Details_Command.UserConfig
{
    public class Role_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }
    }

    public class Role_Dtl_cmd_Handeler : IRequestHandler<Role_Dtl_cmd, Response>
    {
        private readonly IRepositoryAsync<role_cls> _roles;

        public Role_Dtl_cmd_Handeler(IRepositoryAsync<role_cls> roles)
        {
            _roles = roles;
        }

        public async Task<Response> Handle(Role_Dtl_cmd request, CancellationToken cancellationToken)
        {
            role_cls entity = await _roles.GetDetails(request.Id);

            if (entity != null)
            {
                Response response = new()
                {
                    ResponseMessage = "Success",
                    ResponseStatus = "Success",
                    ResponseObject = entity,
                };

                return response;
            }
            return null;
        }
    }
}