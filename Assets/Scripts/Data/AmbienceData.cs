using Infrastructure.Services.MusicService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [System.Serializable]
    
    [CreateAssetMenu(fileName = "AmbienceData", menuName = "Ambience", order = 0)]
    public class AmbienceData : ScriptableObject
    {
        [field: SerializeField] public AmbienceType Type { get; private set; }
        [field: SerializeField] public AssetReferenceT<AudioClip> Clip { get; private set; }
    }
}