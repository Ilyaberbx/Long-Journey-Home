using Logic.Gravity;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sound.Player
{
    [System.Serializable]
    public class FootStepReferenceData
    {
        [SerializeField] private AssetReferenceT<AudioClip>[] stepSoundsReference;
        [SerializeField] private SurfaceType _surfaceType;

        public AssetReferenceT<AudioClip>[] StepSoundsReference => stepSoundsReference;
        public SurfaceType SurfaceType => _surfaceType;
    }
}