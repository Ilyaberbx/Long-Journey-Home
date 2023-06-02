using UnityEngine;

namespace Logic.Player
{
    public class HeroToggle : MonoBehaviour
    {
        [SerializeField] private HeroCameraHolder _cameraHolder;
        [SerializeField] private HeroLook _heroLook;
        [SerializeField] private HeroFreeze _heroFreeze;
        [SerializeField] private HeroLight _heroLight;
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
            _heroLight.enabled = value;
            _heroFreeze.enabled = value; 
            _heroLook.enabled = value;
            _cameraHolder.ToggleCamera(value);
        }
    }
}