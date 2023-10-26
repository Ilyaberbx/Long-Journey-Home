using UnityEngine;

namespace Logic.Player
{
    public class HeroAmbienceSourceContainer : MonoBehaviour
    {
        public AudioSource Source => _source;
        [SerializeField] private AudioSource _source;
    }
}