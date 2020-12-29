using System.Net.NetworkInformation;

namespace QAToolKit.Engine.Probes.Models
{
    /// <summary>
    /// Result of Ping ICMP probe
    /// </summary>
    public class PingResult
    {
        /// <summary>
        /// Ping successful
        /// </summary>
        public IPStatus Success { get; private set; }
        /// <summary>
        /// Reply address
        /// </summary>
        public string ReplyAddress { get; private set; }
        /// <summary>
        /// Roundtrip time in miliseconds
        /// </summary>
        public long RoundTripTime { get; private set; }
        /// <summary>
        /// Time to live
        /// </summary>
        public int? Ttl { get; private set; }
        /// <summary>
        /// Payload size
        /// </summary>
        public long BufferLength { get; private set; }

        /// <summary>
        /// Ping Result constructor
        /// </summary>
        /// <param name="success"></param>
        /// <param name="replyAddress"></param>
        /// <param name="roundTripTime"></param>
        /// <param name="ttl"></param>
        /// <param name="bufferLength"></param>
        public PingResult(IPStatus success, string replyAddress, long roundTripTime, int? ttl, long bufferLength)
        {
            Success = success;
            ReplyAddress = replyAddress;
            RoundTripTime = roundTripTime;
            Ttl = ttl;
            BufferLength = bufferLength;
        }
    }
}
