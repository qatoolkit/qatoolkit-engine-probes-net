namespace QAToolKit.Engine.Probes.Models
{
    /// <summary>
    /// TCP probe result
    /// </summary>
    public class TcpResult
    {
        /// <summary>
        /// Raw Response string
        /// </summary>
        public string ResponseData { get; private set; }

        /// <summary>
        /// TCP probe result constructor
        /// </summary>
        /// <param name="responseData"></param>
        public TcpResult(string responseData)
        {
            ResponseData = responseData;
        }
    }
}
