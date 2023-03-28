namespace Logic.Weapons
{
    public interface IWeapon
    {
        IWeaponAnimator WeaponAnimator { get; }

        void PerformAttack();
    }
}