using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private float _blinkingDuration;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _achieveImage;
        [SerializeField] private TextMeshProUGUI _achieveText;

        private void Awake()
            => _canvasGroup.alpha = 0;

        public void UpdateUI(AchievementDto achievementDto)
        {
            _achieveImage.sprite = achievementDto.Icon;
            _achieveText.text = achievementDto.Text;
        }
        
        public void ShowAchieve()
        {
            BlinkUIGroup()
                .OnComplete(() => Destroy(gameObject));
        }

        private Sequence BlinkUIGroup()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(ToggleUIGroup(1));
            sequence.AppendInterval(_blinkingDuration);
            sequence.Append(ToggleUIGroup(0));
            return sequence;
        }

        private Tween ToggleUIGroup(float value)
            => _canvasGroup.DOFade(value, 1);
    }
}