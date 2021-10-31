using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class User_Inst_cmd : IRequest<Response>
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public class User_Inst_cmd_Handeler : IRequestHandler<User_Inst_cmd, Response>
        {
            private readonly IRepositoryAsync<TblUsermaster> _user;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public User_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<TblUsermaster> user, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _user = user;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(User_Inst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblUsermaster obj = (_mapper.Map<TblUsermaster>(request));
                    obj.CreatedOn = System.DateTime.Now;

                    int result = await _user.SaveAsync(obj, true, "users");

                    if (result > 0)
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "User Created Succesfully " + request.EmailId, "User Created Succesfully " + request.EmailId));
                    }
                    response.ResponseObject = result;
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