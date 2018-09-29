using System;

namespace Baymax.Services.Interface
{
    public interface ILogService
    {
        void Log(Exception ex);

        void Log(string msg);
    }
}