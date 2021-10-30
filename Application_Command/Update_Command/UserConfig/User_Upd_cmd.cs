using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class User_Upd_cmd : IRequest<Response>
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public class User_Upd_cmd_Handeler : IRequestHandler<User_Upd_cmd, Response>
        {
            private readonly IRepositoryAsync<user_cls> _user;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public User_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<user_cls> user, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _user = user;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(User_Upd_cmd request, CancellationToken cancellationToken)
            {
                user_cls obj = (_mapper.Map<user_cls>(request));
                user_cls entity = await _user.GetDetails(obj.UserId);

                if (entity != null)
                {
                    obj.ModifiedOn = System.DateTime.Now;
                    Response response = await _user.UpdateAsync(obj);

                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "User Updated Succesfully " + request.EmailId, "User Updated Succesfully " + request.EmailId));
                    }

                    return response;
                }
                return null;
            }
        }
    }
}