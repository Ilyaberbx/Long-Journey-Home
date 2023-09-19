using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    [CreateAssetMenu(fileName = "AchievementsConfig", menuName = "StaticData/Achievements", order = 0)]
    public class AchievementsConfig : ScriptableObject
    {
        public List<AchievementData> Config;
    }
}