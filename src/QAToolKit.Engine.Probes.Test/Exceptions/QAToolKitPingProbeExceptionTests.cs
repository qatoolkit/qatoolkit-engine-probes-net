using QAToolKit.Engine.Probes.Exceptions;
using System;
using Xunit;

namespace QAToolKit.Engine.Probes.Test.Exceptions
{
    public class QAToolKitPingProbeExceptionTests
    {
        [Fact]
        public static void CreateExceptionTest_Successful()
        {
            var exception = new QAToolKitPingProbeException("my error");

            Assert.Equal("my error", exception.Message);
        }

        [Fact]
        public static void CreateExceptionWithInnerExceptionTest_Successful()
        {
            var innerException = new Exception("Inner");
            var exception = new QAToolKitPingProbeException("my error", innerException);

            Assert.Equal("my error", exception.Message);
            Assert.Equal("Inner", innerException.Message);
        }
    }
}
