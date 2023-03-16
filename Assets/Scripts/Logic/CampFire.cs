using System.Collections;
using DG.Tweening;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic
{
    public class CampFire : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _healCoolDown;
        [SerializeField] private int _healValue;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private string _interactHintText;
        [SerializeField] private Transform _fxContainer;

        private Coroutine _healingRoutine;
        private bool _isFire;

        private void Awake() 
            => _triggerObserver.OnTriggerExited += e => StopHealing();

        private void OnDestroy() 
            => _triggerObserver.OnTriggerExited -= e => StopHealing();

        private void StartHealing(Transform player)
        {
            if (!player.TryGetComponent(out IHealth health) || !player.TryGetComponent(out IFreeze freeze)) return;
            
            _isFire = true;
            _healingRoutine = StartCoroutine(HealingRoutine(health, freeze));
        }

        private void StopHealing()
        {
            if (_healingRoutine == null) return;
            
            StopCoroutine(_healingRoutine);
            DisappearFx();
            _isFire = false;
            _healingRoutine = null;
        }

        private IEnumerator HealingRoutine(IHealth health, IFreeze freeze)
        {
            while (true)
            {
                health.CurrentHealth += _healValue;
                freeze.CurrentFreeze += _healValue;
                yield return new WaitForSeconds(_healCoolDown);
            }
        }

        public void Interact(Transform interactorTransform)
        {
            if (_isFire)
            {
                StopHealing();
                return;
            }

            StartHealing(interactorTransform);
            AppearFx();
        }

        private void AppearFx()
        {
            _fxContainer.gameObject.SetActive(true);
            _fxContainer.DOScale(1f, 1.5f);
        }

        private void DisappearFx()
        {
            _fxContainer.DOScale(1f, 1.5f);
            _fxContainer.gameObject.SetActive(false);
        }

        public string GetInteractText()
            => _interactHintText;
    }
}