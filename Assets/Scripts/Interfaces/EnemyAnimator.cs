using UnityEngine;

namespace Interfaces
{
    public abstract class EnemyAnimator : MonoBehaviour
    {
        public abstract void PlayAttack();
        public abstract void PlayTakeDamage();
        public abstract void PlayDeath();
    }
    
}