using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class SelfDestroying : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeDestroy;

        private void Awake()
        {
            var sequnce = DOTween.Sequence();
            sequnce.AppendInterval(_delayBeforeDestroy);
            sequnce.Append(Disappear());
            sequnce.AppendCallback(Destroy);
        }

        private void Destroy()
            => Destroy(gameObject);

        private Tween Disappear()
            => transform.DOScale(Vector3.zero, 1f);
    }
}