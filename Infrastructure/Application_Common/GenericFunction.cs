using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Application_Common
{
    public static class GenericFunction
    {
        private static readonly char[] charHyphen = new char[] { '-' };
        private static readonly char[] charBlank = new char[0];
        private static readonly char[] charSlash = new char[] { '/' };
        private static readonly char[] charColon = new char[] { ':' };

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string ClassToJson<T>(T req)
        {
            return JsonConvert.SerializeObject(req);
        }

        public static string ListClassToJson<T>(IList<T> req)
        {
            return JsonConvert.SerializeObject(req);
        }

        public static IList<T> JosnToListClass<T>(string req)
        {
            return JsonConvert.DeserializeObject<List<T>>(req);
        }

        public static string ObjectToJson(object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }

        public static DataTable JsonToTable(string jsonString)
        {
            return (DataTable)JsonConvert.DeserializeObject(jsonString, (typeof(DataTable)));
        }

        public static T JsonToClass<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(jsonObject.ToString()); ;
        }

        public static string TableToJson(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        public static XmlDocument JsonToXML(string jsonString)
        {
            //  XmlDocument xml = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString);
            return JsonConvert.DeserializeXmlNode(jsonString);
        }

        public static string XMLToJson(string XML)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML);

            return JsonConvert.SerializeXmlNode(doc);
        }

        public static T ObjectToClass<T>(object input)
        {
            return (T)input;
        }

        //public T ConvertExamp1<T>(object input)
        //{
        //    return (T)Convert.ChangeType(input, typeof(T));
        //}

        public static List<T> ObjectToListClass<T>(object input)
        {
            return (List<T>)input;
        }

        //public List<T> ConvertExamp4<T>(object input)
        //{
        //    return (List<T>)Convert.ChangeType(input, typeof(List<T>));
        //}

        public static List<T> TableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    try
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType), null);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return obj;
        }

        public static string ClassToXML<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            XmlSerializerNamespaces emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (StringWriter stream = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, value, emptyNamepsaces);
                return stream.ToString();
            }
        }

        public static string ObjectToXML(object ClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document,
                                                      // Initializes a new instance of the XmlDocument class.
            XmlSerializer xmlSerializer = new XmlSerializer(ClassObject.GetType());
            // Creates a stream whose backing store is memory.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, ClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static object XMLToObject(string XMLString, object ClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(ClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file
            ClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially DE-serialized, the data is represented by an object without a defined type
            return ClassObject;
        }

        public static string GetSequenceNo(string Prefix)
        {
            Random r = new Random();
            int seqCounter = r.Next(0, 1000);

            string GeneratedSeq = Prefix;
            DateTime now = DateTime.Now;

            GeneratedSeq += now.ToString("yy") + now.ToString("MM") + now.Day.ToString() + now.ToString("HH") + now.ToString("mm") + now.ToString("ss") + seqCounter.ToString("D3");
            return GeneratedSeq;
        }

        public static string DatasetToJson(DataSet Ds)
        {
            // string Json = "";
            return JsonConvert.SerializeObject(Ds, Newtonsoft.Json.Formatting.Indented);
            // return Json;
        }

        public static string key(string Key)
        {
            if (Key == "CMkey")
            {
                return "OMS1@2020#^@El@K";
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<string>(Encrypt_Decrypt.DecryptString(Key, "GNW1@@El@K#^2020", "GNW1@@El@K#^2020"));
                }
                catch
                {
                    return Encrypt_Decrypt.DecryptString(Key, "GNW1@@El@K#^2020", "GNW1@@El@K#^2020");
                }
            }
        }

        public static string IVKey(string Key)
        {
            if (Key == "CMkey")
            {
                return "OMS1@2020#^@El@K";
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<string>(Encrypt_Decrypt.DecryptString(Key, "GNW1@@El@K#^2020", "GNW1@@El@K#^2020"));
                }
                catch
                {
                    return Encrypt_Decrypt.DecryptString(Key, "GNW1@@El@K#^2020", "GNW1@@El@K#^2020");
                }
            }
        }

        public static string TosCrendentials(string input)
        {
            string value = "";
            if (input == "TOS_UsrId")
            {
                value = "tosxml";
            }
            if (input == "TOS_Password")
            {
                value = "tosgnw^!23";
            }
            return value;
        }

        public static string RemoveAllXmlNamespace(string xmlData)
        {
            string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
            MatchCollection matchCol = Regex.Matches(xmlData, xmlnsPattern);

            foreach (Match m in matchCol)
            {
                xmlData = xmlData.Replace(m.ToString(), "");
            }
            return xmlData;
        }
    }
}