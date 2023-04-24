namespace Infrastructure.Services.Pause
{

    public interface IPauseHandler
    {
        void HandlePause(bool isPaused);
    }
}