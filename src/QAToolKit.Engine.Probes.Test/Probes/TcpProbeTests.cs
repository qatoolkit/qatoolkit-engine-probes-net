using QAToolKit.Engine.Probes.Exceptions;
using QAToolKit.Engine.Probes.Probes;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace QAToolKit.Engine.Probes.Test.Probes
{
    public class TcpProbeTests
    {
        [Fact]
        public async Task TcpProbingIpStringTest_Success()
        {
            var tcpProbe = new TcpProbe("tcpbin.com", 4242, new[] { "HELO\n" });
            var result = await tcpProbe.Execute();

            Assert.NotNull(result.ResponseData);
            Assert.Equal("HELO\n", result.ResponseData);
        }

        [Fact]
        public async Task TcpProbingIpTest_Success()
        {
            var tcpProbe = new TcpProbe(IPAddress.Parse("45.79.112.203"), 4242, new[] { "HELO\n" });
            var result = await tcpProbe.Execute();

            Assert.NotNull(result.ResponseData);
            Assert.Equal("HELO\n", result.ResponseData);
        }

        [Fact]
        public async Task TcpProbingHostNameTest_Failure()
        {
            var tcpProbe = new TcpProbe("localhost1", 8888, new[] { "HELO\n" });
            await Assert.ThrowsAsync<QAToolKitTcpProbeException>(async () => await tcpProbe.Execute());
        }

        [Fact]
        public async Task TcpProbingIpStringTest_Failure()
        {
            var tcpProbe = new TcpProbe("127.0.0.0", 8888, new[] { "HELO\n" });
            await Assert.ThrowsAsync<QAToolKitTcpProbeException>(async () => await tcpProbe.Execute());
        }

        [Fact]
        public async Task TcpProbingIpTest_Failure()
        {
            var tcpProbe = new TcpProbe(IPAddress.Parse("127.0.0.1"), 5432, new[] { "HELO\n" });
            await Assert.ThrowsAsync<QAToolKitTcpProbeException>(async () => await tcpProbe.Execute());
        }

        [Fact]
        public void TcpProbingIpNullTest_Failure()
        {
            Assert.Throws<ArgumentNullException>(() => new TcpProbe(IPAddress.Parse(null), 5432, new[] { "HELO\n" }));
        }
    }
}
