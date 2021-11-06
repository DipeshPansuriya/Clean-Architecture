using System.Threading.Tasks;

namespace Application_Core
{
    public interface IResponse_Request
    {
        Task<int> RequestSaveAsync(string ControllerName, string ActionName, string DisplayName, string Userid, string Request);

        Task ResponseSaveAsync(string ControllerName, string ActionName, string DisplayName, string Userid, string Response, int RequestId);
    }
}
