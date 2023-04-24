using Cinemachine;
using UnityEngine;

namespace Logic.Player
{

    public class HeroCameraHolder : MonoBehaviour
    {
        private CinemachinePOV _cameraPov;
        public void Init(CinemachinePOV cameraPov)
            => _cameraPov = cameraPov;

        public void ToggleRecentering(bool value)
        {
            ToggleCamera(value);
            _cameraPov.m_HorizontalRecentering.m_enabled = value;
            _cameraPov.m_VerticalRecentering.m_enabled = value;
        }

        public float CalculateRecenterDuration() =>
            _cameraPov.m_HorizontalRecentering.m_WaitTime +
            _cameraPov.m_HorizontalRecentering.m_RecenteringTime;

        public void ToggleCamera(bool value)
        {
            _cameraPov.m_HorizontalAxis.m_MaxSpeed = value ? 1 : 0;
            _cameraPov.m_VerticalAxis.m_MaxSpeed = value ? 1 : 0;
        }
    }
}