using System;
using Interfaces;
using ProjectSolitude.Enum;
using ProjectSolitude.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectSolitude.Logic.Animations
{
    public class BearAnimator : EnemyAnimator, IAnimationStateReader
    {
        private const int MaxInclusiveAttackCountConst = 3;
        private static readonly string Attack = "Attack";
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly int _movementStateHash = Animator.StringToHash("Movement");
        private readonly int _attackFirstStateHash = Animator.StringToHash("Bear_Attack1");
        private readonly int _attackSecondStateHash = Animator.StringToHash("Bear_Attack2");
        private readonly int _attackThirdStateHash = Animator.StringToHash("Bear_Attack3");
        private readonly int _deathStateHash = Animator.StringToHash("Bear_Death");

        [SerializeField] private Animator _animator;
    
        public AnimatorState State { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
    

        public void PlayDeath()
            => _animator.SetTrigger(Death);

        public override void PlayAttack()
        {
            int rand = Random.Range(1,MaxInclusiveAttackCountConst);
            _animator.SetTrigger(Attack+ rand);
        }

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving()
            => _animator.SetBool(IsMoving, false);

        public void EnterState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitState(int stateHash)
        {
            State = StateFor(stateHash);
            StateExited?.Invoke(State);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            state = AnimatorState.Unknown;

            if (stateHash == _movementStateHash)
                state = AnimatorState.Walk;
            if (stateHash == _attackFirstStateHash)
                state = AnimatorState.Attack;
            if (stateHash == _attackSecondStateHash)
                state = AnimatorState.Attack;
            if (stateHash == _attackThirdStateHash)
                state = AnimatorState.Attack;
            if (stateHash == _deathStateHash)
                state = AnimatorState.Die;

            return state;
        }
    }
}