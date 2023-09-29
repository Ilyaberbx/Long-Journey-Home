using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Task = UnityEditor.VersionControl.Task;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private void Awake()
            => DontDestroyOnLoad(this);

        public Tween Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            return _canvasGroup.DOFade(1, 3f)
                .OnComplete(DisableObject);
        }
        public void Hide() =>
            _canvasGroup.DOFade(0, 3f)
                .OnComplete(DisableObject);

        private void DisableObject() 
            => gameObject.SetActive(false);
    }
}
