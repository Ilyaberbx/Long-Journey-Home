using Data;
using Logic.Spawners;
using UnityEngine;

namespace Logic.Triggers
{
    internal class AchievementTrigger : BaseMarker
    {
        public AchievementType Type => _type;
        [SerializeField] private AchievementType _type;
    }
}