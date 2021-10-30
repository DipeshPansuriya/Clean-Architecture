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
    public class Right_Upd_cmd : IRequest<Response>
    {
        public int RightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public class Right_Upd_cmd_Handeler : IRequestHandler<Right_Upd_cmd, Response>
        {
            private readonly IRepositoryAsync<rights_cls> _rights;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Right_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<rights_cls> rights, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _rights = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Upd_cmd request, CancellationToken cancellationToken)
            {
                rights_cls obj = (_mapper.Map<rights_cls>(request));
                rights_cls entity = await _rights.GetDetails(obj.RightId);

                if (entity != null)
                {
                    obj.ModifiedOn = System.DateTime.Now;
                    Response response = await _rights.UpdateAsync(obj, true, "rights");
                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Updated Succesfully " + request.RightId, "Right Updated Succesfully " + request.RightId));
                    }

                    return response;
                }
                return null;
            }
        }
    }
}