using System.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Services.Hint;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Elements
{
    public class HintView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private IHintService _hintService;

        [Inject]
        public void Construct(IHintService hintService)
        {
            _hintService = hintService;
            
            _hintService.OnHintShowed += ShowHint;
            _hintService.OnHintHide += CloseHint;
        }

        private void OnDestroy()
        {
            _hintService.OnHintShowed -= ShowHint;
            _hintService.OnHintHide -= CloseHint;
        }

        private Task ShowHint(string value)
        {
            _text.text = value;
            return _text.DOFade(1, 2).AsyncWaitForCompletion();
        }

        private Task CloseHint() =>
            _text.DOFade(0, 2)
                .OnComplete(ResetText).AsyncWaitForCompletion();

        private void ResetText()
            => _text.text = string.Empty;
    }
}