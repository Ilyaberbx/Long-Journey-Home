namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public float MouseX { get; }
        bool IsAttackButtonPressed();
        bool IsInteractButtonPressed();
        bool IsInventoryButtonPressed();
        bool IsJumped();
        bool IsSprinting();
        bool IsPauseButtonPressed();
        bool IsReloadButtonPressed();
    }
}
