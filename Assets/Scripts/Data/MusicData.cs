using Infrastructure.Services.MusicService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "MusicData", menuName = "Music", order = 0)]
    public class MusicData : ScriptableObject
    {
        [field: SerializeField] public MusicType Type { get; private set; }
        [field: SerializeField] public AssetReferenceT<AudioClip> Clip { get; private set; }
    }
}