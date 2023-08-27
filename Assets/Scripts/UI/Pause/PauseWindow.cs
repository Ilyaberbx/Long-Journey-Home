using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;

        protected override void SubscribeUpdates()
            => _closeButton.onClick.AddListener(Close);

        protected override void CleanUp()
        {
            base.CleanUp();
            _closeButton.onClick.RemoveListener(Close);
        }
        
    }
}