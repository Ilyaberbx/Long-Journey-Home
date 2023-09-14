using System.Collections.Generic;
using Logic.CutScenes;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Player
{
    public class HeroCutsSceneProcessor : MonoBehaviour
    {
        public bool IsCutSceneActive { get; private set; }
        
        [SerializeField] private TriggerObserver _triggerObserver;

        private List<ICutSceneHandler> _cutSceneHandlers;

        private void Awake()
        {
            _triggerObserver.OnTriggerEntered += Process;
            _cutSceneHandlers = new List<ICutSceneHandler>();
            
            CollectHandlers();
        }

        private void CollectHandlers()
        {
            foreach (ICutSceneHandler handler in GetComponents<ICutSceneHandler>()) 
                _cutSceneHandlers.Add(handler);
        }

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Process;

        private void Process(Collider obj)
        {
            if (!IsCutscene(obj, out ICutScene cutScene)) return;
            
            IsCutSceneActive = true;
            cutScene.StartCutScene(transform, OnCutSceneEnded);
            InformHandlers(IsCutSceneActive);
        }

        private void OnCutSceneEnded()
        {
            IsCutSceneActive = false;
            InformHandlers(IsCutSceneActive);
        }

        private void InformHandlers(bool isInCutScene)
        {
            foreach (ICutSceneHandler handler in _cutSceneHandlers) 
                handler.HandleCutScene(isInCutScene);
        }

        private bool IsCutscene(Collider obj, out ICutScene cutScene) 
            => obj.TryGetComponent(out cutScene);
    }
}