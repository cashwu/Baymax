using System.Threading.Tasks;

namespace Baymax.Services.Interface
{
    public interface ILogBase
    {
        Task LogAsync(System.Exception ex);

        Task LogAsync(string msg);
    }
}