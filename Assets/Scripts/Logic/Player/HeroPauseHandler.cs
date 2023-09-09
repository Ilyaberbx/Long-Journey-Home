using Infrastructure.Services.Pause;
using UnityEngine;

namespace Logic.Player
{
    public class HeroPauseHandler : MonoBehaviour,IPauseHandler
    {
        [SerializeField] private HeroEquiper _heroEquiper;
        [SerializeField] private HeroToggle _heroToggle;
        [SerializeField] private HeroCutSceneHandler _heroCutScene;
        
        public void HandlePause(bool isPaused)
        {
            _heroEquiper.ToggleEquipment(!isPaused);
            
            if(IsInCutScene())
                return;
            
            _heroToggle.Toggle(!isPaused);
        }

        private bool IsInCutScene() 
            => _heroCutScene.IsInCutScene;
    }
}