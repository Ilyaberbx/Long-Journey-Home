using Infrastructure.Services.Input;
using Infrastructure.Services.Pause;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroPause : MonoBehaviour,IPauseHandler
    {
        [SerializeField] private HeroToggle _heroToggle;
        [SerializeField] private HeroCutsSceneProcessor _heroCutsScene;
        private IPauseService _pause;
        private IInputService _input;

        [Inject]
        public void Construct(IInputService input, IPauseService pause)
        {
            _input = input;
            _pause = pause;
        }

        private void Update()
        {
            if (_input.IsPauseButtonPressed()) 
                _pause.SetPaused(true);
        }

        public void HandlePause(bool isPaused)
        {
            if(IsInCutScene(isPaused))
                return;
            
            _heroToggle.Toggle(!isPaused);
        }

        private bool IsInCutScene(bool isPaused) 
            => !isPaused && _heroCutsScene.IsCutSceneActive;
    }
}