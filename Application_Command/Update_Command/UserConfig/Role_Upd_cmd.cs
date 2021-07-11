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
    public class Role_Upd_cmd : IRequest<Response>
    {
        public int RoleId { get; set; }
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }
    }

    public class Role_Upd_cmd_Handeler : IRequestHandler<Role_Upd_cmd, Response>
    {
        private readonly IRepositoryAsync<role_cls> _roles;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public Role_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<role_cls> roles, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._roles = roles;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(Role_Upd_cmd request, CancellationToken cancellationToken)
        {
            role_cls obj = (this._mapper.Map<role_cls>(request));
            role_cls entity = await this._roles.GetDetails(obj.RoleId);

            if (entity != null)
            {
                this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("roles"));

                Response response = await this._roles.UpdateAsync(obj);
                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    NotficationCls notfication = new NotficationCls()
                    {
                        MsgFrom = "dipeshpansuriya@ymail.com",
                        MsgTo = "pansuriya.dipesh@gmail.com",
                        MsgSubject = "Role Update Succesfully " + request.RoleNmae,
                        MsgBody = "Role Update Succesfully " + request.RoleNmae,
                        MsgSatus = NotificationStatus.Pending,
                        MsgType = NotificationType.Mail,
                    };
                    this._backgroundJob.AddEnque<IRepositoryAsync<NotficationCls>>(x => x.AddAsync(notfication));
                }

                return response;
            }
            return null;
        }
    }
}