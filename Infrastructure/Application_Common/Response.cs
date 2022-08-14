using System.Net;

namespace Application_Common
{
    public class Response
    {
        public object ResponseObject { get; set; }
        public bool ResponseStatus { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}