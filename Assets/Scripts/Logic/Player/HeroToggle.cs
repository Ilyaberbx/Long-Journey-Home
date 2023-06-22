using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Player
{
    public class HeroToggle : MonoBehaviour
    {
        [SerializeField] private HeroLook _look;
        [SerializeField] private HeroCameraHolder cameraHolder;
        [SerializeField] private HeroFreeze _freeze;
        [SerializeField] private HeroLight _light;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroDeath _death;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroItemPicker _itemPicker;

        public void Toggle(bool value)
        {
            _attack.enabled = value;
            _mover.enabled = value;
            _death.enabled = value;
            _interactor.enabled = value;
            _itemPicker.enabled = value;
            _look.enabled = value;
            _light.enabled = value;
            _freeze.enabled = value;
            cameraHolder.ToggleCamera(value);
        }
    }
}