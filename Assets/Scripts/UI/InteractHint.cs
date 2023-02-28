using DG.Tweening;
using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InteractHint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactText;
        [SerializeField] private Image _interactLabel;

        private void Awake() 
            => Hide();

        public void Show(IInteractable interactable)
        {
            _interactText.text = interactable.GetInteractText();
            _interactText.DOFade(1,0.5f);
            _interactLabel.DOFade(1,0.5f);
        }

        public void Hide()
        {
            _interactText.DOFade(0,0.5f);
            _interactLabel.DOFade(0,0.5f);
        }
    }
}