using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Details_Command.UserConfig
{
    public class Right_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Right_Dtl_cmd_Handeler : IRequestHandler<Right_Dtl_cmd, Response>
        {
            private readonly IRepositoryAsync<rights_cls> _rights;

            public Right_Dtl_cmd_Handeler(IRepositoryAsync<rights_cls> rights)
            {
                _rights = rights;
            }

            public async Task<Response> Handle(Right_Dtl_cmd request, CancellationToken cancellationToken)
            {
                rights_cls entity = await _rights.GetDetails(request.Id);

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
}