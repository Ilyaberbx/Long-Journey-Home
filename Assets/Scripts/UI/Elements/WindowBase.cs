﻿using Data;
using Infrastructure.Interfaces;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Logic.Inventory;
using Logic.Inventory.Actions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        private IActionListener _closeListener;
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
            => _closeButton.onClick.AddListener(Close);

        public void SubscribeCloseListener(IActionListener listener) 
            => _closeListener = listener;

        private void Close()
        {
            _closeListener.ExecuteAction();
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