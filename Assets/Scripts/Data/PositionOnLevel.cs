namespace Data
{
    [System.Serializable]
    public class PositionOnLevel
    {
        public Vector3Data Position;
        public string Level;

        public PositionOnLevel(string level, Vector3Data position)
        {
            Position = position;
            Level = level;
        }

        public PositionOnLevel(string initialLevel)
        {
            Position = null;
            Level = initialLevel;
        }
    }
}