using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DisappearingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _movingLength;
        [SerializeField] private float _disapperingDuration;

        private float _movingDuration;
        private Direction _direction = Direction.None;

        public void Show(string text, float duration, Direction direction = Direction.None)
        {
            _text.text = text;
            _direction = direction;
            _movingDuration = duration;

            var sequence = DOTween.Sequence();
            sequence.Append(MoveDirected());
            sequence.Append(Disappear());
        }

        private Tween Disappear()
            => transform.DOScale(Vector3.zero, _disapperingDuration);

        private Tween MoveDirected()
        {
            switch (_direction)
            {
                case Direction.Down:
                    return transform.DOMoveY(transform.position.y - _movingLength,
                        _movingDuration - _disapperingDuration);
                case Direction.Up:
                    return transform.DOMoveY(transform.position.y + _movingLength,
                        _movingDuration - _disapperingDuration);
                case Direction.Right:
                    return transform.DOMoveX(transform.position.x + _movingLength,
                        _movingDuration - _disapperingDuration);
                case Direction.Left:
                    return transform.DOMoveX(transform.position.x - _movingLength,
                        _movingDuration - _disapperingDuration);
                case Direction.None:
                    break;
            }

            return null;
        }
    }
}