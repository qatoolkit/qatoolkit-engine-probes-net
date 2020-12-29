using QAToolKit.Engine.Probes.Exceptions;
using QAToolKit.Engine.Probes.Interfaces;
using QAToolKit.Engine.Probes.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Probes.Probes
{
    /// <summary>
    /// TCP Probe
    /// </summary>
    public class TcpProbe : IProbe<TcpResult>
    {
        private readonly int SendTimeout = 10000;
        private readonly int ReceiveTimeout = 10000;
        private readonly string _address;
        private readonly int _port;
        private readonly string[] _messageSequence;

        /// <summary>
        /// TCP probe constructor
        /// </summary>
        /// <param name="iPAddress"></param>
        /// <param name="port"></param>
        /// <param name="messageSequence"></param>
        public TcpProbe(IPAddress iPAddress, int port, string[] messageSequence = null)
        {
            _address = iPAddress.ToString() ?? throw new ArgumentNullException($"{iPAddress} is null.");
            _port = port;
            _messageSequence = messageSequence;
        }

        /// <summary>
        /// TCP probe constructor
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <param name="messageSequence"></param>
        public TcpProbe(string hostName, int port, string[] messageSequence = null)
        {
            _address = hostName ?? throw new ArgumentNullException($"{hostName} is null.");
            _port = port;
            _messageSequence = messageSequence;
        }

        /// <summary>
        /// Execute TCP probe
        /// </summary>
        /// <returns></returns>
        public async Task<TcpResult> Execute()
        {
            try
            {
                using (var client = new TcpClient(_address, _port))
                {
                    client.SendTimeout = SendTimeout;
                    client.ReceiveTimeout = ReceiveTimeout;

                    byte[] data;

                    using (NetworkStream stream = client.GetStream())
                    {
                        foreach (var message in _messageSequence)
                        {
                            data = Encoding.ASCII.GetBytes(message);
                            await stream.WriteAsync(data, 0, data.Length);
                        }

                        var result = new Byte[4096];

                        var bytes = await stream.ReadAsync(result, 0, result.Length);
                        var responseData = Encoding.ASCII.GetString(result, 0, bytes);

                        return new TcpResult(responseData);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new QAToolKitTcpProbeException("ArgumentNullException: {0}", ex);
            }
            catch (SocketException ex)
            {
                throw new QAToolKitTcpProbeException("SocketException: {0}", ex);
            }
            catch (Exception ex)
            {
                throw new QAToolKitTcpProbeException("Exception: {0}", ex);
            }
        }
    }
}
