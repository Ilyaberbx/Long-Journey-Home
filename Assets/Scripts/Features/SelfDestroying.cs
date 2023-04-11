using DG.Tweening;
using UnityEngine;

namespace Features
{
    public class SelfDestroying : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeDestroy;

        private void Awake()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_delayBeforeDestroy);
            sequence.Append(Disappear());
            sequence.AppendCallback(Destroy);
        }

        private void Destroy()
            => Destroy(gameObject);

        private Tween Disappear()
            => transform.DOScale(Vector3.zero, 1f);
    }
}