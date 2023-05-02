using UnityEngine;

namespace Logic.Weapons
{
    public class ReloadableWeaponAnimator : WeaponAnimator, IReloadableWeaponAnimator
    {
        private static readonly int Reload = Animator.StringToHash("Reload");

        public void PlayReload()
            => _animator.SetTrigger(Reload);
    }
}