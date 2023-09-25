using System.Collections;
using DG.Tweening;
using Logic.Enemy;
using Logic.Player;
using Logic.Triggers;
using UnityEngine;

namespace Logic.Level
{
    public class CampFire : MonoBehaviour, IInteractable
    {
        [SerializeField] private SaveTrigger _saveTrigger;
        [SerializeField] private float _healCoolDown;
        [SerializeField] private int _healValue;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private string _interactHintText;
        [SerializeField] private Transform _fxContainer;

        private Coroutine _healingRoutine;
        private bool _isFire;

        private void Awake() 
            => _triggerObserver.OnTriggerExited += _ => StopHealing();

        private void OnDestroy() 
            => _triggerObserver.OnTriggerExited -= _ => StopHealing();

        private void StartHealing(Transform player)
        {
            if (!player.TryGetComponent(out IHealth health) || !player.TryGetComponent(out IFreezable freeze)) return;
            
            _isFire = true;
            _saveTrigger.Save();
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

        private IEnumerator HealingRoutine(IHealth health, IFreezable freezable)
        {
            while (true)
            {
                health.CurrentHealth += _healValue;
                freezable.CurrentFreeze += _healValue;
                yield return new WaitForSeconds(_healCoolDown);
            }
        }

        public void Interact(Transform interactor)
        {
            if (_isFire)
            {
                StopHealing();
                return;
            }

            StartHealing(interactor);
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