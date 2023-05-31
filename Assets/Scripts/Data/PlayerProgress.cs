﻿namespace Data
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
            SaveData = new SaveData();
        }
    }
}