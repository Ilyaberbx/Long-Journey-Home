using System;
using Infrastructure.Services;
using Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public class HeroLook : MonoBehaviour
    {
        private IInputService _input;

        private void Awake() 
            => _input = ServiceLocator.Container.Single<IInputService>();

        private void Update() 
            => Look();

        private void Look() 
            => transform.Rotate(Vector3.up * _input.MouseX);
    }
}