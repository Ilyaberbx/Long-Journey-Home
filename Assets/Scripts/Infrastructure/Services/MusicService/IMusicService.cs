using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services.MusicService
{
    public interface IMusicService : IService
    {
        void PlayAmbience(AmbienceType type);
        void PlayMusic(MusicType type);
        void StopMusic();
        void StopAmbient();

        void Stop();
        void SetAmbienceSource(AudioSource source);
        Task Initialize(AudioMixerGroup globalMixer);
    }
}