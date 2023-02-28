namespace Data
{
    [System.Serializable]
    public class FreezeState
    {
        public float CurrentFreeze;
        public float MaxFreeze;

        public void ResetFreeze()
            => CurrentFreeze = MaxFreeze;
    }
}