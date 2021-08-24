using System;
using System.Net;
using System.IO;

namespace HTTPFetchLibrary {

    public class HttpRequestManager {

        public string HttpRequest(string inUrl)
        {
            string result = String.Empty;

            try
            {
                HttpWebRequest myReq = WebRequest.Create(inUrl) as HttpWebRequest;

                using (WebResponse response = myReq.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (WebException webErr)
            {
                WebExceptionStatus status = webErr.Status;
                result = "WebError: " + status + ", " + webErr.ToString();
            }
            catch (Exception e)
            {
                result = "GenError: " + e.ToString();
            }

            return result;
        }

    }

}
