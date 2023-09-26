using Logic.Player;
using Logic.Weapons;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class PlayerUIActor : UIActor
    {
        [SerializeField] private Bar _flashLightBar;
        [SerializeField] private Bar _freezeBar;
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private InteractHint _interactHint;

        private IHeroLight _light;
        private IFreezable _freezable;
        private IInteractor _interactor;
        private IAmmoUsable _ammoUsableObject;

        public void Construct(IHealth health,IHeroLight light,IFreezable freezable,IInteractor interactor)
        {
            base.Construct(health);

            _freezable = freezable;
            _interactor = interactor;
            _light = light;
            _light.OnIntensityChanged += UpdateFlashLightBar;
            _freezable.OnFreezeChanged += UpdateFreezableBar;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _light.OnIntensityChanged -= UpdateFlashLightBar;
            _freezable.OnFreezeChanged -= UpdateFreezableBar;
        }

        private void Update() 
            => ShowHint(_interactor.GetInteractableObject());

        public void RegisterAmmoShowableObject(IAmmoUsable ammoUsable)
        {
            _ammoUsableObject = ammoUsable;
            ToggleAmmoBar(true);
            UpdateAmmoBar();
            _ammoUsableObject.OnAmmoChanged += UpdateAmmoBar;
            _ammoUsableObject.OnDispose += () => ToggleAmmoBar(false);
        }

        private void ToggleAmmoBar(bool isActive) 
            => _ammoText.gameObject.SetActive(isActive);

        private void UpdateAmmoBar() 
            => _ammoText.text = _ammoUsableObject.CurrentAmmo + "/" + _ammoUsableObject.MaxAmmo;

        private void ShowHint(IInteractable interactable)
        {
            if (interactable != null)
                _interactHint.Show(interactable);
            else
                _interactHint.Hide();
        }

        private void UpdateFreezableBar()
            => _freezeBar.SetValue(_freezable.CurrentFreeze,_freezable.MaxFreeze);
        

        private void UpdateFlashLightBar() 
            => _flashLightBar.SetValue(_light.CurrentIntensity,_light.MaxIntensity);
    }
}