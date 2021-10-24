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
    public class Right_Inst_cmd : IRequest<Response>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public class Right_Inst_cmd_Handeler : IRequestHandler<Right_Inst_cmd, Response>
        {
            private readonly IRepositoryAsync<rights_cls> _right;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Right_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<rights_cls> rights, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _right = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Inst_cmd request, CancellationToken cancellationToken)
            {
                rights_cls obj = (_mapper.Map<rights_cls>(request));
                obj.CreatedOn = System.DateTime.Now;

                Response response = await _right.SaveAsync(obj, true, "rights");

                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Created Succesfully " + request.RoleId, "Right Created Succesfully " + request.RoleId);
                }

                return response;
            }
        }
    }
}