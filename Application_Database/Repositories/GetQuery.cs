using Application_Core.Repositories;
using Application_Genric;
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
        public string GetDBQuery(string FolderName, string queryID, DynamicParameters param)
        {
            string Query = string.Empty;
            string QueryParameters = string.Empty;
            try
            {
                string text = File.ReadAllText(APISetting.XMLFilePath + FolderName + @"\XMLQuery.xml");
                XDocument xDoc = XDocument.Parse(text);

                if (xDoc != null)
                {
                    Query = xDoc.Elements("Queries").Elements("QUERY").Where(t => t.Attribute("NAME").Value == queryID).FirstOrDefault().Value;
                }

                if (param != null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (param != null)
                    {
                        foreach (string name in param.ParameterNames)
                        {
                            dynamic pValue = param.Get<dynamic>(name);
                            sb.AppendFormat("{0}={1},", "@" + name, "''" + pValue.ToString() + "''");
                            Query = Query.Replace("@" + name, pValue.ToString());
                        }
                    }

                    StringBuilder pam = new StringBuilder();

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        pam.Append(sb.ToString().TrimEnd(','));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error in GetQuery.CS :- " + ex.Message, ex.InnerException);
            }
            return Query;
        }
    }
}