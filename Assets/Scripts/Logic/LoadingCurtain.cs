using System;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private Guid _uid;

        private void Awake()
            => DontDestroyOnLoad(this);

        public Tween Show()
        {
            StopPreviousTween();
            CreateNewId();

            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            return _canvasGroup.DOFade(1, 2f);
        }

        private void CreateNewId() 
            => _uid = Guid.NewGuid();

        private void StopPreviousTween() 
            => DOTween.Kill(_uid);

        public void Hide() =>
            _canvasGroup.DOFade(0, 2f)
                .OnComplete(DisableObject).SetId(_uid);

        private void DisableObject()
            => gameObject.SetActive(false);
    }
}