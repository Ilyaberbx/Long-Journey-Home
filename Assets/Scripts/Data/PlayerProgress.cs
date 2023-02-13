namespace Data
{
    [System.Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public WorldData WorldData;
        public Stats Stats;
        public KillData KillData;
        public FlashLightState FlashLightState;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HealthState = new HealthState();
            Stats = new Stats();
            KillData = new KillData();
            FlashLightState = new FlashLightState();
        }

    }
}