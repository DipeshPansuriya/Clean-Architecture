using Application_Core;
using Application_Core.Repositories;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Threading.Tasks;

namespace Application_Infrastructure
{
    public class Response_Request : IResponse_Request
    {
        private readonly IDapper<Response_Request> _dapper_request;

        private readonly ILogger<Response_Request> _logger;
        public Response_Request(IDapper<Response_Request> dapper_request, ILogger<Response_Request> logger)
        {
            this._dapper_request = dapper_request;
            this._logger = logger;
        }

        public async Task<int> RequestSaveAsync(string ControllerName, string ActionName, string DisplayName, string Userid, string Request)
        {
            try
            {
                string Query = "Insert Into tbl_API_Request(ControllerName, ActionName, DisplayName, Userid, Request, RequestDate) Values (N'" + ControllerName + "', N'" + ActionName + "', N'" + DisplayName + "', N'" + Userid + "', N'" + Request + "', GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int)";

                string id = await _dapper_request.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text);
                return int.Parse(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
            return 0;
        }

        public async Task ResponseSaveAsync(string ControllerName, string ActionName, string DisplayName, string Userid, string Response, int RequestId)
        {
            try
            {
                bool res = Response.Contains(",\"ResponseStatus\":true,") == true ? true : false;

                string Query = "Insert Into tbl_API_Response(Response, RequestId, ResponseDate, ReponseStatus, ControllerName, ActionName, DisplayName, UserId) Values (N'" + Response + "', N'" + RequestId + "', GETDATE(), N'" + res.ToString() + "', N'" + ControllerName + "', N'" + ActionName + "', N'" + DisplayName + "', N'" + Userid + "'); SELECT CAST(SCOPE_IDENTITY() as int)";

                string id = await _dapper_request.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text);

            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
        }

    }
}
