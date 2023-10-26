using System.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.MusicService
{
    public class MusicService : IMusicService
    {
        private const float AmbientSourceVolume = 0.2f;
        private const float MusicSourceVolume = 1f;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly AudioSource _musicSource;
        private AudioSource _ambientSource;

        public MusicService(IStaticDataService staticDataService, AudioSource musicSource, IAssetProvider assetProvider)
        {
            _staticDataService = staticDataService;
            _musicSource = musicSource;
            _assetProvider = assetProvider;
        }

        public void SetAmbienceSource(AudioSource source) 
            => _ambientSource = source;

        public async void PlayAmbience(AmbienceType type)
        {
            AssetReferenceT<AudioClip> clipReference = _staticDataService.GetAmbienceData(type).Clip;
            AudioClip clip = await _assetProvider.Load(clipReference);
            SmoothDampAudio(_ambientSource, clip, AmbientSourceVolume);
        }

        public async void PlayMusic(MusicType type)
        {
            AssetReferenceT<AudioClip> clipReference = _staticDataService.GetMusicData(type).Clip;
            AudioClip clip = await _assetProvider.Load(clipReference);
            SmoothDampAudio(_musicSource, clip, MusicSourceVolume);
        }

        public void StopMusic()
            => SmoothToggle(_musicSource, 0);

        public void StopAmbient()
            => SmoothToggle(_ambientSource, 0);

        public void Stop()
        {
            StopMusic();
            StopAmbient();
        }

        private void SmoothDampAudio(AudioSource source, AudioClip clip,float volume)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(SmoothToggle(source, 0));
            sequence.AppendCallback(() => ChangeClip(source, clip));
            sequence.Append(SmoothToggle(source, volume));
        }

        private Tween SmoothToggle(AudioSource source, float value)
            => source.DOFade(value, 1f);

        private void ChangeClip(AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }
    }
}