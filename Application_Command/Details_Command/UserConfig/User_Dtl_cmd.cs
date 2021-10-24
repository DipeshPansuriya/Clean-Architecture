using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Details_Command.UserConfig
{
    public class User_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class User_Dtl_cmd_Handeler : IRequestHandler<User_Dtl_cmd, Response>
        {
            private readonly IRepositoryAsync<user_cls> _user;

            public User_Dtl_cmd_Handeler(IRepositoryAsync<user_cls> user)
            {
                _user = user;
            }

            public async Task<Response> Handle(User_Dtl_cmd request, CancellationToken cancellationToken)
            {
                user_cls entity = await _user.GetDetails(request.Id);

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