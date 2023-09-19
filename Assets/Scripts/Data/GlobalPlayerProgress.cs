namespace Data
{
    [System.Serializable]
    public class GlobalPlayerProgress 
    {
        public AchievementsData Achievements;
        public EndingsData Endings;

        public GlobalPlayerProgress()
        {
            Achievements = new AchievementsData();
            Endings = new EndingsData();
        }
    }
}