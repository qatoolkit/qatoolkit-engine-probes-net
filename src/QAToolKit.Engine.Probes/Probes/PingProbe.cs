using QAToolKit.Engine.Probes.Interfaces;
using QAToolKit.Engine.Probes.Models;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Probes.Probes
{
    /// <summary>
    /// Ping probe
    /// </summary>
    public class PingProbe : IProbe<PingResult>
    {
        private readonly string _address;
        private readonly string Data = "0000000000000000000000000000000";
        private readonly int Timeout = 120;

        /// <summary>
        /// Ping probe constructor
        /// </summary>
        /// <param name="hostName"></param>
        public PingProbe(string hostName)
        {
            _address = hostName;
        }

        /// <summary>
        /// Ping probe constructor
        /// </summary>
        /// <param name="ipAddress"></param>
        public PingProbe(IPAddress ipAddress)
        {
            _address = ipAddress.ToString();
        }

        /// <summary>
        /// Execute ping probe
        /// </summary>
        /// <returns></returns>
        public async Task<PingResult> Execute()
        {
            using (Ping pingSender = new Ping())
            {
                PingOptions options = new PingOptions
                {
                    DontFragment = true
                };

                byte[] buffer = Encoding.ASCII.GetBytes(Data);
                PingReply reply = await pingSender.SendPingAsync(_address, Timeout, buffer, options);

                return new PingResult(reply.Status, reply.Address.ToString(), reply.RoundtripTime, reply.Options?.Ttl, reply.Buffer.Length);
            }
        }
    }
}
