namespace Baymax.Services.Interface
{
    public interface IBackgroundProcessService
    {
        void DoWork();

        void StopWork();
    }
}