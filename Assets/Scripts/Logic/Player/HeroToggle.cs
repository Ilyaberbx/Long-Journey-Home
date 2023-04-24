using Infrastructure.Services.Pause;
using UnityEngine;

namespace Logic.Player
{
    public class HeroToggle : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroDeath _death;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroItemPicker _itemPicker;
        public void HandlePause(bool isPaused) 
            => Toggle(!isPaused);

        private void Toggle(bool value)
        {
            _attack.enabled = value;
            _mover.enabled = value;
            _death.enabled = value;
            _interactor.enabled = value;
            _itemPicker.enabled = value;
        }
    }
}