namespace Infrastructure.Services.Pause
{

    public interface IPauseService : IService
    {
        bool IsPaused { get; }
        bool CanBePaused { get; set; }
        void Register(IPauseHandler handler);
        void SetPaused(bool isPaused);
        void CleanUp();
        void UnRegister(IPauseHandler handler);
    }
}
