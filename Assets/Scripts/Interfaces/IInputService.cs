namespace Interfaces
{
    public interface IInputService : IService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
  
        public float MouseY { get; }
        public float MouseX { get; }

        public bool IsAttackButtonPressed();

        public bool IsInteractButtonPressed();
        public bool IsJumped();
        public bool IsSprinting();
    }
}
