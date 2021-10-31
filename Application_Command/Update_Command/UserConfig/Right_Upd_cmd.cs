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
            private readonly IRepositoryAsync<TblRightmaster> _rights;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Right_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<TblRightmaster> rights, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _rights = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Upd_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRightmaster obj = (_mapper.Map<TblRightmaster>(request));
                    TblRightmaster entity = await _rights.GetDetails(obj.RightId);

                    if (entity != null)
                    {
                        obj.ModifiedOn = System.DateTime.Now;
                        int result = await _rights.UpdateAsync(obj, true, "rights");
                        if (result > 0)
                        {
                            Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Updated Succesfully " + request.RightId, "Right Updated Succesfully " + request.RightId));
                        }
                        response.ResponseObject = result;
                    }
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