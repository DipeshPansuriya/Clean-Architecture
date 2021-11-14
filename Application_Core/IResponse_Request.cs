using System.Threading.Tasks;

namespace Application_Core
{
    public interface IResponse_Request
    {
        Task<int> RequestSaveAsync(string Scheme, string Path, string QueryString, string Userid, string Request);

        Task ResponseSaveAsync(string Userid, string Response, int RequestId);
    }
}