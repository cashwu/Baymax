namespace Baymax.Services.Interface
{
    public interface ILogService
    {
        void Log(System.Exception ex);

        void Log(string msg);
    }
}