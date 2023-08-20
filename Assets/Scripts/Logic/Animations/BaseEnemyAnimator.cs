using UnityEngine;

namespace Logic.Animations
{
    public abstract class BaseEnemyAnimator : MonoBehaviour
    {
        public abstract void PlayAttack(int index);
        public abstract void PlayRandomAttack();
        public abstract void PlayTakeDamage();
        public abstract void PlayDeath();
        public abstract void Move(float speed);
        public abstract void StopMoving();
    }
    
}