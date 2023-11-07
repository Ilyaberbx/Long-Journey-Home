using Sound.SoundSystem;
using Sound.SoundSystem.Operators.Variations;
using UnityEngine;

namespace Logic.Enemy
{

    public class SoundAlongAgent : MonoBehaviour
    {
        [SerializeField] private SoundOperations _soundOperations;

        private void OnStep() 
            => _soundOperations.PlaySound<MoveOperator>();

        private void OnRoared()
            => Debug.Log("asdasd");

    }
}