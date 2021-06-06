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
            _mapper = mapper;
            _demoCustomer = demoCustomer;
            _cache = cache;
            _backgroundJob = backgroundClient;
            _notfication = notfication;
        }

        public async Task<Response> Handle(Demo_Customer_Inst_cmd request, CancellationToken cancellationToken)
        {
            var obj = (this._mapper.Map<Demo_Customer>(request));

            _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("democust"));
            //_backgroundJob.AddSchedule<ICacheService>(x => x.RemoveCache("democust"), RecuringTime.Seconds, 2);

            var response = await _demoCustomer.AddAsync(obj);
            if (response != null && response.ResponseStatus.ToLower() == "success")
            {
                NotficationCls notfication = new NotficationCls()
                {
                    MsgFrom = "dipeshpansuriya@ymail.com",
                    MsgTo = "pansuriya.dipesh@gmail.com",
                    MsgSubject = "Test",
                    MsgBody = "Test Body",
                    MsgSatus = NotificationStatus.Pending,
                    MsgType = NotificationType.Mail,
                };
                //await _notfication.AddAsync(notfication);
                _backgroundJob.AddEnque<IRepositoryAsync<NotficationCls>>(x => x.AddAsync(notfication));
            }

            return response;
        }
    }
}