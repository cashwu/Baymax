using System;
using System.Threading.Tasks;

namespace Baymax.Services.Interface
{
    public interface ILogBase
    {
        Task LogAsync(Exception ex, string env);

        Task LogAsync(string msg, string env);
    }
}