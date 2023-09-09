using UnityEngine;

namespace Logic.Animations
{
    public class BearAnimator : EnemyAnimator
    {
        private static readonly int Roar = Animator.StringToHash("Roar");
        
        public void PlayRoar() 
            => _animator.SetTrigger(Roar);
    }
}