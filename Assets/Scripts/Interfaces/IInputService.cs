using Interfaces;

namespace ProjectSolitude.Interfaces
{
    public interface IInputService : IService
    {
        public float Horizontal { get; }
        public float Vertical { get; }

        public bool IsJumped();
        public bool IsSprinting();
    }
}
