using Application_Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application_Infrastructure
{
    public class Response_Request : IResponse_Request
    {
        private readonly ILogger<Response_Request> _logger;

        public Response_Request(ILogger<Response_Request> logger)
        {
            _logger = logger;
        }

        public async Task<int> RequestSaveAsync(string Scheme, string Path, string QueryString, string Userid, string Request)
        {
            try
            {
                string Query = "Insert Into tbl_API_Request(Scheme, Path, QueryString, Userid, Request, RequestDate) Values (N'" + Scheme + "', N'" + Path + "', N'" + QueryString + "', N'" + Userid + "', N'" + Request + "', GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int)";

                string id = "0";
                //await _dapper_request.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text);
                return int.Parse(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return 0;
        }

        public async Task ResponseSaveAsync(string Userid, string Response, int RequestId)
        {
            try
            {
                bool res = Response.Contains("responseStatus\": true,") == true ? true : false;
                if (res == false)
                {
                    res = Response.Contains("responseStatus\":true,") == true ? true : false;
                }

                string Query = "Insert Into tbl_API_Response(Response, RequestId, ResponseDate, ReponseStatus,  UserId) Values (N'" + Response + "', N'" + RequestId + "', GETDATE(), N'" + res.ToString() + "', N'" + Userid + "'); SELECT CAST(SCOPE_IDENTITY() as int)";
                //await _dapper_request.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}