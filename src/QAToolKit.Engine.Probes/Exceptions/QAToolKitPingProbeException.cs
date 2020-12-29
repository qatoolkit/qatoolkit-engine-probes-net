using System;
using System.Runtime.Serialization;

namespace QAToolKit.Engine.Probes.Exceptions
{
    /// <summary>
    /// QA Toolkit TCP probe exception
    /// </summary>
    [Serializable]
    public class QAToolKitPingProbeException : Exception
    {
        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        public QAToolKitPingProbeException(string message) : base(message)
        {
        }

        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        public QAToolKitPingProbeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        protected QAToolKitPingProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
