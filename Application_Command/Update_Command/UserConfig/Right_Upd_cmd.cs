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
    public class Right_Upd_cmd : IRequest<Response>
    {
        public int RoleId { get; set; }
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }
    }

    public class Right_Upd_cmd_Handeler : IRequestHandler<Right_Upd_cmd, Response>
    {
        private readonly IRepositoryAsync<rights_cls> _rights;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public Right_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<rights_cls> rights, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._rights = rights;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(Right_Upd_cmd request, CancellationToken cancellationToken)
        {
            rights_cls obj = (this._mapper.Map<rights_cls>(request));
            rights_cls entity = await this._rights.GetDetails(obj.RightId);

            if (entity != null)
            {
                this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("rights"));

                Response response = await this._rights.UpdateAsync(obj);
                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    NotficationCls notfication = new NotficationCls()
                    {
                        MsgFrom = "dipeshpansuriya@ymail.com",
                        MsgTo = "pansuriya.dipesh@gmail.com",
                        MsgSubject = "Right Update Succesfully " + request.RoleId,
                        MsgBody = "Right Update Succesfully " + request.RoleId,
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