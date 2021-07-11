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
    public class User_Upd_cmd : IRequest<Response>
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }

    public class User_Upd_cmd_Handeler : IRequestHandler<User_Upd_cmd, Response>
    {
        private readonly IRepositoryAsync<user_cls> _user;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public User_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<user_cls> user, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._user = user;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(User_Upd_cmd request, CancellationToken cancellationToken)
        {
            user_cls obj = (this._mapper.Map<user_cls>(request));
            user_cls entity = await this._user.GetDetails(obj.UserId);

            if (entity != null)
            {
                this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("users"));

                Response response = await this._user.UpdateAsync(obj);
                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    NotficationCls notfication = new NotficationCls()
                    {
                        MsgFrom = "dipeshpansuriya@ymail.com",
                        MsgTo = "pansuriya.dipesh@gmail.com",
                        MsgSubject = "User Update Succesfully " + request.EmailId,
                        MsgBody = "User Update Succesfully " + request.EmailId,
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