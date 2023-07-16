using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class PositionOnLevel
    {
        public List<Vector3Data> Positions;
        public List<string> Levels;
        public string CurrentLevel;
        
        public PositionOnLevel(string initialCurrentLevel)
        {
            CurrentLevel = initialCurrentLevel;
            Positions = new List<Vector3Data>();
            Levels = new List<string>();
        }
    }
}