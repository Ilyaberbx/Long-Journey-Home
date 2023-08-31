using DG.Tweening;
using UI.Elements;
using UnityEngine;

namespace UI.GameOver
{
    public class GameOverWindow : WindowBase
    {
        [SerializeField] private float _tweenDuration;
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Show() 
            => _canvasGroup.DOFade(1, _tweenDuration);
    }
}