using Interfaces;
using UnityEngine;

namespace Logic.Weapons
{
    public class WeaponAnimator : MonoBehaviour, IWeaponAnimator
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private Animator _animator;
        
        public void PlayAttack()
            => _animator.SetTrigger(Attack);

        public void SetAnimatorSpeed(float speed) 
            => _animator.speed = speed;
    }
}