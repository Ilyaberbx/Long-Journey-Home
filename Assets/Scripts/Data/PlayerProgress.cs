namespace Data
{
    [System.Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public FreezeState FreezeState;
        public WorldData WorldData;
        public KillData KillData;
        public SaveData SaveData;
        public PickUpLootData PickUpLootData;
        public FlashLightState FlashLightState;
        public InventoryData InventoryData;
        public DialogueData DialogueData;
        public CutSceneData CutSceneData;
        public bool IsFirstLoad = true;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HealthState = new HealthState();
            KillData = new KillData();
            FlashLightState = new FlashLightState();
            FreezeState = new FreezeState();
            InventoryData = new InventoryData();
            PickUpLootData = new PickUpLootData();
            CutSceneData = new CutSceneData();
            DialogueData = new DialogueData();
            SaveData = new SaveData();
        }
    }
}