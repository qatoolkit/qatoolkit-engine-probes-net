using System.Net;

namespace QAToolKit.Engine.Probes.Models
{
    /// <summary>
    /// HTTP probe result
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// Response HTTP status code
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// Response body
        /// </summary>
        public string ResponseBody { get; private set; }

        /// <summary>
        /// HTTP result constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="responseBody"></param>
        public HttpResult(HttpStatusCode statusCode, string responseBody)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}
