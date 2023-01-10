using UnityEngine;
using DG.Tweening;

namespace ProjectSolitude.Logic
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
            var sequnce = DOTween.Sequence();
            sequnce.Append(_canvasGroup.DOFade(0, 1f));
            sequnce.AppendCallback(DisabeObject);
        }

        private void DisabeObject() 
            => gameObject.SetActive(false);
    }
}
