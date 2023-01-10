namespace ProjectSolitude.Data
{
    [System.Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public WorldData WorldData;
          
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HealthState = new HealthState();
        }
    }
}