namespace Logic.Weapons
{
    public interface IWeaponAnimator
    {
        void PlayAttack(int index);
        void SetAnimatorSpeed(float speed);
    }
}