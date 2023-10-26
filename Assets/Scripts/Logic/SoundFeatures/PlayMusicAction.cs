using Infrastructure.Services.MusicService;
using Logic.Level;
using UnityEngine;
using Zenject;

namespace Logic.SoundFeatures
{
    public class PlayMusicAction : MonoBehaviour, IAction
    {
        [SerializeField] private MusicType _musicType;
        private IMusicService _musicService;

        [Inject]
        public void Construct(IMusicService musicService) 
            => _musicService = musicService;

        public void Execute() 
            => _musicService.PlayMusic(_musicType);
    }
}