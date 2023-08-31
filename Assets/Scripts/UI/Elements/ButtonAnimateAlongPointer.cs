using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ButtonAnimateAlongPointer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _animationDuration;
        [SerializeField] private TextMeshProUGUI _hintText;

        private void Awake()
        {
            if (_hintText != null)
                _hintText.alpha = 0;
        }

        public void ChangeScale(float value)
            => _button.transform.DOScale(value, _animationDuration);

        public void ToggleHint(bool value)
            => _hintText.DOFade(value ? 255f : 0f, _animationDuration);
    }
}