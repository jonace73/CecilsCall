

using System.Threading.Tasks;

namespace CecilsCall.Services
{
    public interface ICommWithServer
    {
        Task<bool> SendByDependency(string msg, string serverMsg);
        Task<bool> CloseConnectionsByDependency();
    }
}
