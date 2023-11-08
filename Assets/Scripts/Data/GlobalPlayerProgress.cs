using UnityEngine.Serialization;

namespace Data
{
    [System.Serializable]
    public class GlobalPlayerProgress
    {
        public AchievementsData Achievements;
        public EndingsData Endings;
        public bool IsSecondLoad;

        public GlobalPlayerProgress()
        {
            Achievements = new AchievementsData();
            Endings = new EndingsData();
        }
    }
}