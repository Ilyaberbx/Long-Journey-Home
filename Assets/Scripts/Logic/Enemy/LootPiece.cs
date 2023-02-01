using System.Collections;
using Data;
using DG.Tweening;
using UI;
using UnityEngine;

namespace Logic.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _lootVisualObject;
        [SerializeField] private DisappearingText _lootText;
        [SerializeField] private float _delayBeforeDestroy;

        private Loot _loot;
        private WorldData _worldData;
        private bool _isPicked;

        public void Initialize(Loot loot)
            => _loot = loot;

        public void Construct(WorldData worldData) 
            => _worldData = worldData;

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
            _worldData.LootData.Collect(_loot);
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(_delayBeforeDestroy);
            Destroy(gameObject);
        }

        private void Disappear() 
            => _lootVisualObject.transform.DOScale(Vector3.zero, 1f);

        private void ShowText()
            => _lootText.Show("+" + _loot.Value,_delayBeforeDestroy,Direction.Up);
    }
}