using Infrastructure.Services.Input;
using Infrastructure.Services.Pause;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroPauser : MonoBehaviour
    {
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
    }
}