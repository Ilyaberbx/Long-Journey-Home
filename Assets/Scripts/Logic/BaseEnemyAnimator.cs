using UnityEngine;

namespace Interfaces
{
    public abstract class BaseEnemyAnimator : MonoBehaviour
    {
        public abstract void PlayAttack();
        public abstract void PlayTakeDamage();
        public abstract void PlayDeath();
        public abstract void Move(float speed);
        public abstract void StopMoving();
    }
    
}