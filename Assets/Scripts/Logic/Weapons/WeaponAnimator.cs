using UnityEngine;

namespace Logic.Weapons
{
    public class WeaponAnimator : MonoBehaviour, IWeaponAnimator
    {
        private const string Attack = "Attack";

        [SerializeField] protected Animator _animator;
        private static readonly int SingeAttack = Animator.StringToHash(Attack);

        public void PlayAttack(int index)
        {
            if(index == 0)
                _animator.SetTrigger(SingeAttack);
            else
                _animator.SetTrigger(Attack + index);
        }

        public void SetAnimatorSpeed(float speed)
            => _animator.speed = speed;
    }
}