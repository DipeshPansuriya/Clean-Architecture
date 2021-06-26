using Application_Core.Background;
using Application_Core.Cache;
using Application_Core.Repositories;
using Application_Domain;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command
{
    public class Demo_Customer_Inst_cmd : IRequest<Response>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Demo_Custome_Insert_Handeler : IRequestHandler<Demo_Customer_Inst_cmd, Response>
    {
        private readonly IRepositoryAsync<Demo_Customer> _demoCustomer;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;
        private readonly IRepositoryAsync<NotficationCls> _notfication;

        public Demo_Custome_Insert_Handeler(IMapper mapper, IRepositoryAsync<Demo_Customer> demoCustomer, ICacheService cache, IBackgroundJob backgroundClient, IRepositoryAsync<NotficationCls> notfication)
        {
            this._mapper = mapper;
            this._demoCustomer = demoCustomer;
            this._cache = cache;
            this._backgroundJob = backgroundClient;
            this._notfication = notfication;
        }

        public async Task<Response> Handle(Demo_Customer_Inst_cmd request, CancellationToken cancellationToken)
        {
            Demo_Customer obj = (this._mapper.Map<Demo_Customer>(request));

            this._backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("democust"));
            //_backgroundJob.AddSchedule<ICacheService>(x => x.RemoveCache("democust"), RecuringTime.Seconds, 2);

            Response response = await this._demoCustomer.AddAsync(obj);
            if (response != null && response.ResponseStatus.ToLower() == "success")
            {
                NotficationCls notfication = new()
                {
                    MsgFrom = "dipeshpansuriya@ymail.com",
                    MsgTo = "pansuriya.dipesh@gmail.com",
                    MsgSubject = "Test",
                    MsgBody = "Test Body",
                    MsgSatus = NotificationStatus.Pending,
                    MsgType = NotificationType.Mail,
                };
                //await _notfication.AddAsync(notfication);
                this._backgroundJob.AddEnque<IRepositoryAsync<NotficationCls>>(x => x.AddAsync(notfication));
            }

            return response;
        }
    }
}