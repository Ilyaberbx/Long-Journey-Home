using UnityEngine;

namespace Logic.Weapons
{
    public class WeaponAnimator : MonoBehaviour, IWeaponAnimator
    {
        private const string Attack = "Attack";

        [SerializeField] protected Animator _animator;

        public void PlayAttack(int index) 
            => _animator.SetTrigger(Attack + index);

        public void SetAnimatorSpeed(float speed)
            => _animator.speed = speed;
    }
}