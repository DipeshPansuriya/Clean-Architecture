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
    public class User_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }
    }

    public class User_Del_cmd_Handeler : IRequestHandler<User_Del_cmd, Response>
    {
        private readonly IRepositoryAsync<user_cls> _user;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public User_Del_cmd_Handeler(IMapper mapper, IRepositoryAsync<user_cls> user, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._user = user;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(User_Del_cmd request, CancellationToken cancellationToken)
        {
            user_cls entity = await this._user.GetDetails(request.Id);

            if (entity != null)
            {
                this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("users"));

                entity.IsDeleted = true;

                Response response = await this._user.UpdateAsync(entity);
                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    NotficationCls notfication = new NotficationCls()
                    {
                        MsgFrom = "dipeshpansuriya@ymail.com",
                        MsgTo = "pansuriya.dipesh@gmail.com",
                        MsgSubject = "User Deleted Succesfully " + entity.EmailId,
                        MsgBody = "User Deleted Succesfully " + entity.EmailId,
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