namespace Application_Core
{
    public interface IResponse_Request
    {
        public int RequestSave(string filename, string userid, string request);

        public void RepsponseSave(string response, string requestid);
    }
}
