using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application_Common
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestClient
    {
        public CookieContainer cookies = new CookieContainer();
        private HttpWebResponse response;
        private HttpWebRequest request;

        public string URL { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestClient()
        {
            URL = "";
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public RestClient(string url)
        {
            URL = url;
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public RestClient(string url, HttpVerb method)
        {
            URL = url;
            Method = method;
            ContentType = "application/json";
            PostData = "";
        }

        public RestClient(string url, HttpVerb method, string postData)
        {
            URL = url;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }

        public async Task<string> MakeRequestSync()
        {
            return await MakeRequestSync("");
        }

        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public async Task<string> MakeRequestSync(string parameters)
        {
            string url = URL + parameters;

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (request.CookieContainer == null)
            {
                request.CookieContainer = cookies;
            }

            //request.Credentials = new NetworkCredential("dipesh.pansuriya", "kale_1234");
            //WebProxy webProxy = new WebProxy("10.22.3.44", true)
            //{
            //    Credentials = new NetworkCredential("dipesh.pansuriya", "kale_1234"),
            //    UseDefaultCredentials = false
            //};
            //request.Proxy = webProxy;

            string responseValue = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    //var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (Stream writeStream = await request.GetRequestStreamAsync())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    CookieCollection setCookies = cookies.GetCookies(request.RequestUri);
                    CookieContainer cc = new CookieContainer();
                    cc.Add(setCookies);
                    request.CookieContainer = cc;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        //throw new ApplicationException(message);
                    }

                    // grab the response
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse temp = ex.Response;
                if (temp != null)
                {
                    responseValue = new StreamReader(temp.GetResponseStream()).ReadToEnd().ToString();
                }
                else
                {
                    responseValue = "Error " + ex.Message.ToString();
                }
            }
            return responseValue;
        }

        public string MakeRequest(string parameters)
        {
            string url = URL + parameters;

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (request.CookieContainer == null)
            {
                request.CookieContainer = cookies;
            }

            //request.Credentials = new NetworkCredential("dipesh.pansuriya", "kale_1234");
            //WebProxy webProxy = new WebProxy("10.22.3.44", true)
            //{
            //    Credentials = new NetworkCredential("dipesh.pansuriya", "kale_1234"),
            //    UseDefaultCredentials = false
            //};
            //request.Proxy = webProxy;

            string responseValue = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    //var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (Stream writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    CookieCollection setCookies = cookies.GetCookies(request.RequestUri);
                    CookieContainer cc = new CookieContainer();
                    cc.Add(setCookies);
                    request.CookieContainer = cc;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        //throw new ApplicationException(message);
                    }

                    // grab the response
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse temp = ex.Response;
                if (temp != null)
                {
                    responseValue = new StreamReader(temp.GetResponseStream()).ReadToEnd().ToString();
                }
                else
                {
                    responseValue = "Error " + ex.Message.ToString();
                }
            }
            return responseValue;
        }
    }
}