namespace Application_Domain
{
    public class Response
    {
        public Response()
        {
            this.ResponseStatus = "Unkonwn"; this.ResponseMessage = "Success"; this.OtherMessage = ""; this.ResponseId = 0;
        }

        public string ResponseMessage { get; set; }
        public string OtherMessage { get; set; }
        public object ResponseObject { get; set; }
        public string ResponseStatus { get; set; }
        public int ResponseId { get; set; }
    }
}