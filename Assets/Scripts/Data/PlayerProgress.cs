using ProjectSolitude.Data;

namespace Data
{
    [System.Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public WorldData WorldData;
        public Stats Stats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HealthState = new HealthState();
            Stats = new Stats();
            KillData = new KillData();
        }

    }
}