using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using AutoMapper;
using MediatR;
using System;
using System.Net;
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
            private readonly IRepositoryAsync<TblRightmaster> _right;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Right_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<TblRightmaster> rights, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _right = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Inst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRightmaster obj = (_mapper.Map<TblRightmaster>(request));
                    obj.CreatedOn = System.DateTime.Now;

                    int result = await _right.SaveAsync(obj, true, "rights");

                    if (result > 0)
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Created Succesfully " + request.RoleId, "Right Created Succesfully " + request.RoleId));
                    }
                    response.ResponseObject = result;
                }
                catch (Exception ex)
                {
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return response;
            }
        }
    }
}