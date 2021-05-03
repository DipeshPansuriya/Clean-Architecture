using Application_Core.Interfaces;
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

    public class Demo_Custome_Handeler : IRequestHandler<Demo_Customer_Inst_cmd, Response>
    {
        private readonly IRepositoryAsync<Demo_Customer> _demoCustomer;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly ICacheService _cache;
        private readonly IBackgroundJob _backgroundJob;

        public Demo_Custome_Handeler(IMapper mapper, IRepositoryAsync<Demo_Customer> demoCustomer, IUnitOfWork unitOfWork, ICacheService cache, IBackgroundJob backgroundClient)
        {
            _mapper = mapper;
            _demoCustomer = demoCustomer;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _backgroundJob = backgroundClient;
        }

        public async Task<Response> Handle(Demo_Customer_Inst_cmd request, CancellationToken cancellationToken)
        {
            var obj = (this._mapper.Map<Demo_Customer>(request));

            _backgroundJob.AddEnque<ICacheService>(x => x.RemoveCache("democust"));
            //_backgroundJob.AddSchedule<ICacheService>(x => x.RemoveCache("democust"), RecuringTime.Seconds, 2);

            await _demoCustomer.AddAsync(obj);
            var response = await _unitOfWork.Commit(cancellationToken);

            return response;
        }
    }
}