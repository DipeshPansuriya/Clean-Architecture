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
    public class User_Inst_cmd : IRequest<Response>
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }

    public class User_Inst_cmd_Handeler : IRequestHandler<User_Inst_cmd, Response>
    {
        private readonly IRepositoryAsync<user_cls> _user;
        private readonly IMapper _mapper;
        private readonly INotificationMsg _notificationMsg;

        public User_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<user_cls> user, INotificationMsg notificationMsg)
        {
            _mapper = mapper;
            _user = user;
            _notificationMsg = notificationMsg;
        }

        public async Task<Response> Handle(User_Inst_cmd request, CancellationToken cancellationToken)
        {
            user_cls obj = (_mapper.Map<user_cls>(request));
            obj.CreatedOn = System.DateTime.Now;
            Response response = await _user.SaveAsync(obj, true, "users");
            if (response != null && response.ResponseStatus.ToLower() == "success")
            {
                _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "User Created Succesfully " + request.EmailId, "User Created Succesfully " + request.EmailId);
            }

            return response;
        }
    }
}