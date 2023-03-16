using System.Collections;
using Data;
using DG.Tweening;
using Features;
using UI;
using UI.Elements;
using UnityEngine;

namespace Logic.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private DisappearingText _lootText;
        [SerializeField] private float _dissapearingDuration;
        [SerializeField] private float _delayBeforeDestroy;

        private FlashLightLoot _flashLightLoot;
        private FlashLightState _flashLightState;
        private bool _isPicked;

        public void Initialize(FlashLightLoot flashLightLoot)
            => _flashLightLoot = flashLightLoot;

        public void Construct(FlashLightState flashLightState) 
            => _flashLightState = flashLightState;

        private void OnTriggerEnter(Collider other)
            => PickUp();

        private void PickUp()
        {
            if (_isPicked) return;

            CollectToWorldData();
            Disappear();
            ShowText();
            StartCoroutine(DestroyRoutine());
        }

        private void CollectToWorldData()
        {
            _isPicked = true;
            _flashLightState.Add(_flashLightLoot);
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(_delayBeforeDestroy);
            Destroy(gameObject);
        }

        private void Disappear() 
            => transform.DOScale(Vector3.zero, _dissapearingDuration);

        private void ShowText()
            => _lootText.Show("+" + _flashLightLoot.Value,_delayBeforeDestroy,Direction.Up);
    }
}