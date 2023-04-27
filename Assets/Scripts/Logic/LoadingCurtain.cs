using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private void Awake()
            => DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
        public void Hide()
        {
            _canvasGroup.DOFade(0, 1f)
                .OnComplete(DisableObject);
        }

        private void DisableObject() 
            => gameObject.SetActive(false);
    }
}
