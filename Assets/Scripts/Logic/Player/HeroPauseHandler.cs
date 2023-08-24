using Infrastructure.Services.Input;
using Infrastructure.Services.Pause;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroPauseHandler : MonoBehaviour,IPauseHandler
    {
        [SerializeField] private HeroEquiper _heroEquiper;
        [SerializeField] private HeroToggle _heroToggle;
        [SerializeField] private HeroCutsSceneProcessor _heroCutsScene;
        
        public void HandlePause(bool isPaused)
        {
            _heroEquiper.ToggleEquipment(!isPaused);
            
            if(IsInCutScene(isPaused))
                return;
            
            _heroToggle.Toggle(!isPaused);
        }

        private bool IsInCutScene(bool isPaused) 
            => !isPaused && _heroCutsScene.IsCutSceneActive;
    }
}