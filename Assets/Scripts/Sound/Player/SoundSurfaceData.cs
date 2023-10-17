using Logic.Gravity;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sound.Player
{
    [System.Serializable]
    public class SoundSurfaceData
    {
        [field: SerializeField] public AssetReferenceT<AudioClip>[] Clips { get; private set; }
        [field: SerializeField] public SurfaceType Surface { get; private set; }
    }
}