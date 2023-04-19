using System;
using System.Threading.Tasks;
using Data;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        private Action _close;
        protected PlayerProgress _progress;

        [Inject]
        public void Construct(IPersistentProgressService progressService) 
            => _progress = progressService.PlayerProgress;

        private void Awake()
            => OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() 
            => CleanUp();

        protected virtual void OnAwake()
            => _closeButton?.onClick.AddListener(Close);

        public void SubscribeCloseListener(Action onClose) 
            => _close = onClose;

        public virtual void Close()
        {
            _close?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void CleanUp()
        {
        }
    }
}