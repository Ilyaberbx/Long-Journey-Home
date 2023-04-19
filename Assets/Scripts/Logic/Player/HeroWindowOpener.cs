using System.Collections;
using Cinemachine;
using Infrastructure.Interfaces;
using Infrastructure.Services.Input;
using Logic.Inventory;
using Logic.Inventory.Actions;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroWindowOpener : MonoBehaviour
    {
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroLook _look;
        [SerializeField] private HeroInteractor _interactor;
        [SerializeField] private HeroAttack _attack;

        private IInputService _input;
        private IWindowService _windowService;
        private bool _isWindowOpen;

        private CinemachinePOV _cameraPov;

        [Inject]
        public void Construct(IInputService input, IWindowService windowService)
        {
            _input = input;
            _windowService = windowService;
        }

        public void Init(CinemachinePOV cameraPov)
            => _cameraPov = cameraPov;

        private void Update()
        {
            if (_isWindowOpen)
                return;

            if (_input.IsInventoryButtonPressed())
            {
                _isWindowOpen = true;
                Cursor.lockState = CursorLockMode.Confined;
                ToggleHero(false);
                _windowService.Open(WindowType.Inventory, OnWindowClose);
            }
        }

        private void ToggleHero(bool value)
        {
            _mover.enabled = value;
            _look.enabled = value;
            _interactor.enabled = value;
            _attack.enabled = value;
        }

        public void OnWindowClose()
        {
            StartCoroutine(RecenterCameraRoutine());
            Cursor.lockState = CursorLockMode.Locked;
        }

        private IEnumerator RecenterCameraRoutine()
        {
            ToggleRecentering(true);
            yield return new WaitForSeconds(CalculateRecenterDuration());
            ToggleRecentering(false);
            ToggleHero(true);
            _isWindowOpen = false;
        }

        private void ToggleRecentering(bool value)
        {
            _cameraPov.m_HorizontalAxis.m_MaxSpeed = value ? 0 : 1;
            _cameraPov.m_VerticalAxis.m_MaxSpeed = value ? 0 : 1;
            _cameraPov.m_HorizontalRecentering.m_enabled = value;
            _cameraPov.m_VerticalRecentering.m_enabled = value;
        }

        private float CalculateRecenterDuration() =>
            _cameraPov.m_HorizontalRecentering.m_WaitTime +
            _cameraPov.m_HorizontalRecentering.m_RecenteringTime;
    }
}