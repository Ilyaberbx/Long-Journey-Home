using Logic.Gravity;
using UnityEngine;

namespace Sound.Player
{

    public class FootStepSoundData
    {
        public AudioClip[] StepSounds { get; }
        public SurfaceType SurfaceType { get; }

        public FootStepSoundData(AudioClip[] stepSounds, SurfaceType surfaceType)
        {
            StepSounds = stepSounds;
            SurfaceType = surfaceType;
        }
    }
}