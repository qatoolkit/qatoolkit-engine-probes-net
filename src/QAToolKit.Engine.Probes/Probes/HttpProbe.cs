using QAToolKit.Engine.Probes.Interfaces;
using QAToolKit.Engine.Probes.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Probes.Probes
{
    /// <summary>
    /// HTTP probe
    /// </summary>
    public class HttpProbe : IProbe<HttpResult>
    {
        private readonly Uri _url;
        private readonly HttpMethod _httpMethod;
        private readonly bool _validateCertificate;

        /// <summary>
        /// HTTP probe constructor
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="validateCertificate"></param>
        public HttpProbe(Uri url, HttpMethod httpMethod, bool validateCertificate = true)
        {
            _url = url;
            _httpMethod = httpMethod;
            _validateCertificate = validateCertificate;
        }

        /// <summary>
        /// Execute a probe
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResult> Execute()
        {
            using (var HttpHandler = new HttpClientHandler())
            {

                if (!_validateCertificate && (_url.Scheme == Uri.UriSchemeHttp || _url.Scheme == Uri.UriSchemeHttps))
                {
                    HttpHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    HttpHandler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };
                }

                using (var httpClient = new HttpClient(HttpHandler))
                {
                    var request = new HttpRequestMessage(_httpMethod, _url);

                    var response = await httpClient.SendAsync(request);

                    return new HttpResult(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
