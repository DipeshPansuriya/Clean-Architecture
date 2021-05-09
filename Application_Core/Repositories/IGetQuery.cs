using Dapper;

namespace Application_Core.Repositories
{
    public interface IGetQuery
    {
        string GetDBQuery(string FolderName, string queryID);

        string GetDBQuery(string FolderName, string queryID, DynamicParameters param);
    }
}