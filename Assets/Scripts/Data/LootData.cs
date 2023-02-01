namespace Data
{
    [System.Serializable]
    public class LootData
    {
        public int Collected;

        public void Collect(Loot loot) 
            => Collected += loot.Value;
    }
}