using System.Net;

namespace Application_Domain
{
    public class Response
    {
        public Response()
        {
            ResponseStatus = "Unkonwn"; ResponseMessage = "Success"; OtherMessage = ""; ResponseId = 0;
        }

        public string ResponseMessage { get; set; }
        public string OtherMessage { get; set; }
        public object ResponseObject { get; set; }
        public string ResponseStatus { get; set; }
        public int ResponseId { get; set; }
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
    }
}