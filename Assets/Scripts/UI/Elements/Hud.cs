using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class Hud : MonoBehaviour, IHud
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Open() 
            => _canvasGroup.DOFade(1, 1f);

        public void Hide() 
            => _canvasGroup.DOFade(0, 1f);
    }
}