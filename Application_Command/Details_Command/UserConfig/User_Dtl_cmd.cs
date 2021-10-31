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
    public class User_Dtl_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class User_Dtl_cmd_Handeler : IRequestHandler<User_Dtl_cmd, Response>
        {
            private readonly IRepositoryAsync<TblUsermaster> _user;

            public User_Dtl_cmd_Handeler(IRepositoryAsync<TblUsermaster> user)
            {
                _user = user;
            }

            public async Task<Response> Handle(User_Dtl_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblUsermaster entity = await _user.GetDetails(request.Id);

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