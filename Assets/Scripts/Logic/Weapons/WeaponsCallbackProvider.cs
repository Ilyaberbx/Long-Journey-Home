using Logic.Player;
using UnityEngine;

namespace Logic.Weapons
{
    public class WeaponsCallbackProvider : MonoBehaviour
    {
        [SerializeField] private HeroAttack _attack;

        private void OnAttack() 
            => _attack.OnAttack();
    }
}