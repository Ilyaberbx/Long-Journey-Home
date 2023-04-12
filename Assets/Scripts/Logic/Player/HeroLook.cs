using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroLook : MonoBehaviour
    {
        private IInputService _input;

        [Inject]
        public void Construct(IInputService input) 
            => _input = input;

        private void Update() 
            => Look();

        private void Look() 
            => transform.Rotate(Vector3.up * _input.MouseX);
    }
}