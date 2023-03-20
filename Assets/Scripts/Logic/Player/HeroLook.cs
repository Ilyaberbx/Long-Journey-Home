using Infrastructure.Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class HeroLook : MonoBehaviour
    {
        private IInputService _input;

        public void Construct(IInputService input) 
            => _input = input;

        private void Update() 
            => Look();

        private void Look() 
            => transform.Rotate(Vector3.up * _input.MouseX);
    }
}