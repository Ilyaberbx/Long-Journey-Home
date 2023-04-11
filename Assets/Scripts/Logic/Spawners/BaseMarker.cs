using UnityEngine;

namespace Logic.Spawners
{
    public abstract class BaseMarker : MonoBehaviour
    {
        [SerializeField] private Color _indicatorColor;
        public Color IndicatorColor => _indicatorColor;
    }
}