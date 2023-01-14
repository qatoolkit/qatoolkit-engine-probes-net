using Microsoft.Extensions.Logging;
using QAToolKit.Engine.Probes.Probes;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace QAToolKit.Engine.Probes.Test.Probes
{
    public class PingProbeTests
    {
        private readonly ILogger<PingProbeTests> _logger;

        public PingProbeTests(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<PingProbeTests>();
        }

        [IgnoreOnGithubFact]
        public async Task PingHostNameTest_Success()
        {
            var pinger = new PingProbe("52.217.36.203", 500);
            var result = await pinger.Execute();

            Assert.Equal(IPStatus.Success, result.Success);
        }

        [IgnoreOnGithubFact]
        public async Task PingParseIPTest_Success()
        {
            var pinger = new PingProbe(IPAddress.Parse("52.217.36.203"), 500);
            var result = await pinger.Execute();

            Assert.Equal(IPStatus.Success, result.Success);
        }

        [Fact]
        public async Task PingHostNameTest_Fails()
        {
            var pinger = new PingProbe("googlexzy.com");

            await Assert.ThrowsAsync<PingException>(async () => await pinger.Execute());
        }

        [Fact]
        public async Task PingIPStringInvalidTest_Fails()
        {
            var pinger = new PingProbe("0.0.0.0");

            await Assert.ThrowsAsync<ArgumentException>(async () => await pinger.Execute());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task PingIPStringNullTest_Fails(string ip)
        {
            var pinger = new PingProbe(ip);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await pinger.Execute());
        }
        
        [Fact]
        public async Task PingIPInvalidTest_Fails()
        {
            var pinger = new PingProbe(IPAddress.Parse("0.0.0.0"));

            await Assert.ThrowsAsync<ArgumentException>(async () => await pinger.Execute());
        }

        [Fact]
        public void PingIPNullTest_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new PingProbe(IPAddress.Parse(null)));
        }
    }
}
