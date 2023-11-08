using System.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

namespace Infrastructure.Services.MusicService
{
    public class MusicService : IMusicService
    {
        private const float AmbientSourceVolume = 0.2f;
        private const float MusicSourceVolume = 1f;
        private const string MusicSourceAddress = "MusicAndAmbienceSource";
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;
        private AudioSource _musicSource;
        private AudioSource _ambientSource;

        public MusicService(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public async Task Initialize(AudioMixerGroup globalMixer)
        {
            GameObject musicSourceObject = await CreateMusicSource();
            Object.DontDestroyOnLoad(musicSourceObject);
            _musicSource = musicSourceObject.GetComponent<AudioSource>();
            _musicSource.outputAudioMixerGroup = globalMixer;
        }

        private async Task<GameObject> CreateMusicSource()
        {
            GameObject musicSourcePrefab = await _assetProvider.Load<GameObject>(MusicSourceAddress);
            GameObject musicSourceObject = Object.Instantiate(musicSourcePrefab);
            musicSourceObject.name = "Music";
            return musicSourceObject;
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
            Debug.Log("Music played: " + clip.name);
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

        private void SmoothDampAudio(AudioSource source, AudioClip clip, float volume)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(SmoothToggle(source, 0));
            sequence.AppendCallback(() => ChangeClip(source, clip));
            sequence.Append(SmoothToggle(source, volume));
        }

        private Tween SmoothToggle(AudioSource source, float value)
            => source.DOFade(value, 3f);

        private void ChangeClip(AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }
    }
}