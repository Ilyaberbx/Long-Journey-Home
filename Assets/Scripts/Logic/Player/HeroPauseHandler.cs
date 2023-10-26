using Infrastructure.Services.Pause;
using UnityEngine;

namespace Logic.Player
{
    public class HeroPauseHandler : MonoBehaviour, IPauseHandler
    {
        public bool IsHandlePause { get; set; } = true;

        [SerializeField] private HeroEquiper _heroEquiper;
        [SerializeField] private HeroToggle _heroToggle;
        [SerializeField] private HeroCutSceneHandler _heroCutScene;

        public void HandlePause(bool isPaused)
        {
            _heroEquiper.ToggleEquipment(!isPaused);

            if (IsInCutScene())
                return;
            
            if (!IsHandlePause)
                return;

            _heroToggle.Toggle(!isPaused);
        }

        private bool IsInCutScene()
            => _heroCutScene.IsInCutScene;
    }
}