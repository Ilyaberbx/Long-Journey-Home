using Enums;
using UnityEngine;

namespace Logic.CutScenes
{

    [System.Serializable]
    public class CutSceneCameraTransitionData
    {
        [SerializeField] private AnimationCurve _blendCurve;
        [SerializeField] private float _blendTime;
        [SerializeField] private GameCameraType _type;

        public AnimationCurve BlendCurve => _blendCurve;
        public float BlendTime => _blendTime;
        public GameCameraType Type => _type;
    }
}