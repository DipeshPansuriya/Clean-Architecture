using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class Role_Inst_cmd : IRequest<Response>
    {
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }
    }

    public class Role_Inst_cmd_Handeler : IRequestHandler<Role_Inst_cmd, Response>
    {
        private readonly IRepositoryAsync<role_cls> _roles;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public Role_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<role_cls> roles, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._roles = roles;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(Role_Inst_cmd request, CancellationToken cancellationToken)
        {
            role_cls obj = (this._mapper.Map<role_cls>(request));

            this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("roles"));

            Response response = await this._roles.AddAsync(obj);
            if (response != null && response.ResponseStatus.ToLower() == "success")
            {
                NotficationCls notfication = new()
                {
                    MsgFrom = "dipeshpansuriya@ymail.com",
                    MsgTo = "pansuriya.dipesh@gmail.com",
                    MsgSubject = "Role Create Succesfully " + request.RoleNmae,
                    MsgBody = "Role Create Succesfully " + request.RoleNmae,
                    MsgSatus = NotificationStatus.Pending,
                    MsgType = NotificationType.Mail,
                };
                this._backgroundJob.AddEnque<IRepositoryAsync<NotficationCls>>(x => x.AddAsync(notfication));
            }

            return response;
        }
    }
}