using Infrastructure.Services.Settings;
using Logic.CutScenes;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Player
{
    public class HeroCutsSceneProcessor : MonoBehaviour
    {
        public bool IsCutSceneActive { get; private set; }
        
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private HeroToggle _heroToggle;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += Process;

        private void OnDestroy() 
            => _triggerObserver.OnTriggerEntered -= Process;

        private void Process(Collider obj)
        {
            if (!IsCutscene(obj, out ICutScene cutScene)) return;
            
            IsCutSceneActive = true;
            _heroToggle.Toggle(false);
            cutScene.StartCutScene(transform, OnCutSceneEnded);
        }

        private void OnCutSceneEnded()
        {
            IsCutSceneActive = false;
            _heroToggle.Toggle(true);
        }

        private bool IsCutscene(Collider obj, out ICutScene cutScene) 
            => obj.TryGetComponent(out cutScene);
    }
}