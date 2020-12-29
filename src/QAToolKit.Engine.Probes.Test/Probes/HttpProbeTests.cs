using QAToolKit.Engine.Probes.Probes;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Probes.Test.Probes
{
    public class HttpProbeTests
    {
        [Theory]
        [InlineData("https://qatoolkitapi.azurewebsites.net/swagger/index.html")]
        [InlineData("https://qatoolkitapi.azurewebsites.net/api/bicycles?api-version=1")]
        public async Task HttpProbingUrlTest_Success(string url)
        {
            var httpProbe = new HttpProbe(new Uri(url), HttpMethod.Get);
            var result = await httpProbe.Execute();

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.ResponseBody);
        }

        [Theory]
        [InlineData("https://swagger-demo.qatoolkit.io/swagger/index.html")]
        public async Task HttpProbingUrlNoSSLValidationTest_Success(string url)
        {
            var httpProbe = new HttpProbe(new Uri(url), HttpMethod.Get, false);
            var result = await httpProbe.Execute();

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.ResponseBody);
        }

        [Theory]
        [InlineData(null)]
        public void HttpProbingNullTest_Fails(string url)
        {
            Assert.Throws<ArgumentNullException>(() => new HttpProbe(new Uri(url), HttpMethod.Get));
        }

        [Theory]
        [InlineData("")]
        public void HttpProbingEmptyTest_Fails(string url)
        {
            Assert.Throws<UriFormatException>(() => new HttpProbe(new Uri(url), HttpMethod.Get));
        }
    }
}
