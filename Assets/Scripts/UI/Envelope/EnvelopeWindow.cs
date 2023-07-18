using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Envelope
{
    public class EnvelopeWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _envelopeText;
        [SerializeField] private Button _closeButton;
        protected override void SubscribeUpdates() 
            => _closeButton.onClick.AddListener(Close);

        protected override void CleanUp()
        {
            base.CleanUp();
            _closeButton.onClick.RemoveListener(Close);
        }

        public void UpdateContent(string text) 
            => _envelopeText.text = text;
    }
}