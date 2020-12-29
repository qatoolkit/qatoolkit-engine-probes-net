using System;
using System.Runtime.Serialization;

namespace QAToolKit.Engine.Probes.Exceptions
{
    /// <summary>
    /// QA Toolkit TCP probe exception
    /// </summary>
    [Serializable]
    public class QAToolKitTcpProbeException : Exception
    {
        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        public QAToolKitTcpProbeException(string message) : base(message)
        {
        }

        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        public QAToolKitTcpProbeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// QA Toolkit core exception
        /// </summary>
        protected QAToolKitTcpProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
