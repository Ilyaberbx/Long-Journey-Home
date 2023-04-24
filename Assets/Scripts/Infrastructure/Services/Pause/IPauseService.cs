namespace Infrastructure.Services.Pause
{

    public interface IPauseService : IService
    {
        bool IsPaused { get; }
        void Register(IPauseHandler handler);
        void UnRegister(IPauseHandler handler);
        void SetPaused(bool isPaused);
        void CleanUp();
    }
}
