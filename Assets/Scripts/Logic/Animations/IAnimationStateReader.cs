using ProjectSolitude.Enum;

namespace Logic.Animations
{
    public interface IAnimationStateReader
    {
        void EnterState(int stateHash);
        void ExitState(int stateHash);
        AnimatorState State { get; }
    }
}