using System.Net;

namespace Application_Common
{
    public class Response
    {
        public Response()
        {
            ResponseStatus = true;
        }

        public object ResponseObject { get; set; }

        public bool ResponseStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}