using Application_Core.Repositories;
using Application_Domain;
using Dapper;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Application_Database.Repositories
{
    public class GetQuery : IGetQuery
    {
        public string GetDBQuery(string FolderName, string queryID)
        {
            string Query = string.Empty;
            try
            {
                string text = File.ReadAllText(APISetting.XMLFilePath + FolderName + @"\XMLQuery.xml");
                XDocument xDoc = XDocument.Parse(text);

                if (xDoc != null)
                {
                    Query = xDoc.Elements("Queries").Elements("QUERY").Where(t => t.Attribute("NAME").Value == queryID).FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error in GetQuery.CS :- " + ex.Message, ex.InnerException);
            }
            return Query;
        }

        public string GetDBQuery(string FolderName, string queryID, DynamicParameters param)
        {
            string DBQuery = string.Empty;
            string Query = string.Empty;
            string QueryParameters = string.Empty;
            try
            {
                string text = File.ReadAllText(APISetting.XMLFilePath + FolderName);
                XDocument xDoc = XDocument.Parse(text);

                if (xDoc != null)
                {
                    Query = xDoc.Elements("Queries").Elements("QUERY").Where(t => t.Attribute("NAME").Value == queryID).FirstOrDefault().Value;
                }

                StringBuilder sb = new StringBuilder();
                if (param != null)
                {
                    foreach (string name in param.ParameterNames)
                    {
                        dynamic pValue = param.Get<dynamic>(name);
                        sb.AppendFormat("{0}={1},", "@" + name, "''" + pValue.ToString() + "''");
                        Query = Query.Replace("@" + name, "'" + pValue.ToString() + "'");
                    }
                }

                StringBuilder pam = new StringBuilder();

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    pam.Append(sb.ToString().TrimEnd(','));
                }

                DBQuery = " BEGIN TRY \n" +
                " SET NOCOUNT ON \n" +
                Query +
                " END TRY \n" +
                " BEGIN CATCH \n" +
                " Declare @InputParameters VARCHAR(MAX); \n" +
                " SET @InputParameters = '" + pam.ToString() + "' \n" +
                " EXEC InsertDBErrorLog @InputParameters,'" + APISetting.XMLFilePath + FolderName + "','" + queryID + "'\n " +
                " END CATCH; \n";
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error in GetQuery.CS :- " + ex.Message, ex.InnerException);
            }
            return DBQuery;
        }
    }
}