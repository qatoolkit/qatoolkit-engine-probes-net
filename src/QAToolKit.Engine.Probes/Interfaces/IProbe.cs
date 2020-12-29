using System.Threading.Tasks;

namespace QAToolKit.Engine.Probes.Interfaces
{
    /// <summary>
    /// Probe interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProbe<T>
    {
        /// <summary>
        /// Execute probe
        /// </summary>
        /// <returns></returns>
        Task<T> Execute();
    }
}
