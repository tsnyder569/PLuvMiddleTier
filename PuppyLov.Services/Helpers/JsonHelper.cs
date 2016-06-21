using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Ventera.CSR.Demo.Helpers
{
    /// <summary>
    /// Helper class for JASON calls processing
    /// </summary>
    public class JsonHelper
    {
        #region GET Methods

        /// <summary>
        /// Get Jason Response
        /// </summary>
        /// <param name="url">URL of the GET operation</param>
        /// <returns>Return's response as string value</returns>
        public string GetAsJson(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = Resource.GetMethod;
            request.ContentType = Resource.ContentTypeGet;
            request.Timeout = Convert.ToInt32(Resource.Timeout);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                string data = null;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(
                         String.Format(Resource.RequestFailedFormat,
                                       url, response.StatusCode, 
                                       response.StatusDescription));
                }

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }

                return data;
            }
        }

        #endregion GET

        #region POST Methods

        /// <summary>
        /// Makes the HttpWebRequest using HTTP Method POST
        /// </summary>
        /// <param name="url">URL of the POST operation</param>
        /// <param name="request">Request Object</param>
        /// <returns>Returns Object of type Tr</returns>
        public Tr MakePostRequest<Ti, Tr>(string url, Ti request)
            where Ti : class
            where Tr : class
        {
            var result = default(Tr);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = Resource.ContentTypePost;
            httpWebRequest.Method = Resource.PostMethod;
            httpWebRequest.Timeout = Convert.ToInt32(Resource.Timeout);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //The below line will append the request as the root element
                dynamic wrapper = new { request = request };
                string jsonInput = JsonConvert.SerializeObject(wrapper);

                streamWriter.Write(jsonInput);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var response = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Tr>(response);
                }
            }
            return result;
        }

        /// <summary>
        /// Makes the HttpWebRequest using HTTP Method POST
        /// </summary>
        /// <param name="url">URL of the POST operation</param>
        /// <param name="querystring">Request part of Query string</param>
        /// <returns>Returns Object of type Tr</returns>
        public Tr MakePostRequestQueryString<Tr>(string url, string querystring)
            where Tr : class
        {
            Tr result = default(Tr);

            if (!string.IsNullOrWhiteSpace(querystring))
            {
                url = string.Concat(url, "?", querystring);
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = Resource.ContentTypePost;
            httpWebRequest.Method = Resource.PostMethod;
            httpWebRequest.Timeout = Convert.ToInt32(Resource.Timeout);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var response = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Tr>(response);
                }
            }
            return result;
        }

        #endregion POST
    }
}
