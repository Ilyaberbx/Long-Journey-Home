using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class Hud : MonoBehaviour, IHud
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Open() 
            => _canvasGroup.DOFade(0.25f, 0.5f);

        public void Hide() 
            => _canvasGroup.DOFade(0, 0.5f);
    }
}