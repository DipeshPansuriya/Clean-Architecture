using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Interfaces
{
    public interface IGetQuery
    {
        string GetDBQuery(string FolderName, string queryID);

        string GetDBQuery(string FolderName, string queryID, DynamicParameters param);
    }
}