using Application_Common;
using Application_Core;
using Application_Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Application_Infrastructure
{
    public class Response_Request : IResponse_Request
    {
        private readonly ILogger<Response_Request> logger;
        private readonly IDapper<Response_Request> dapper;

        public Response_Request(ILogger<Response_Request> logger, IDapper<Response_Request> dapper)
        {
            this.logger = logger;
            this.dapper = dapper;
        }

        public async Task<int> RequestSaveAsync(string Scheme, string Path, string QueryString, string Userid, string Request)
        {
            try
            {
                string Query = "Insert Into APIRequest(Scheme, Path, QueryString, Userid, Request, RequestDate) Values (N'" + Scheme + "', N'" + Path + "', N'" + QueryString + "', N'" + Userid + "', N'" + Request + "', GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int)";

                string id = "0";
                id = await dapper.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text, APISetting.LogDBConnection);
                return int.Parse(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return 0;
        }

        public async Task ResponseSaveAsync(string Userid, string Response, int RequestId)
        {
            try
            {
                bool res = Response.Contains("ResponseStatus\": true,") == true ? true : false;
                if (res == false)
                {
                    res = Response.Contains("ResponseStatus\":true,") == true ? true : false;
                }

                string Query = "Insert Into APIResponse(Response, RequestId, ResponseDate, ReponseStatus,  UserId) Values (N'" + Response + "', N'" + RequestId + "', GETDATE(), N'" + res.ToString() + "', N'" + Userid + "'); SELECT CAST(SCOPE_IDENTITY() as int)";
                await dapper.ExecuteScalarAsync(Query, null, System.Data.CommandType.Text, APISetting.LogDBConnection);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}