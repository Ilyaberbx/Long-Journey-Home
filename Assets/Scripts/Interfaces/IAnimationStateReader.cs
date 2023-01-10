using ProjectSolitude.Enum;

namespace Interfaces
{
    public interface IAnimationStateReader
    {
        void EnterState(int stateHash);
        void ExitState(int stateHash);
        AnimatorState State { get; }
    }
}