using System.Collections.Generic;
using Infrastructure.StateMachine;

namespace Data
{
    [System.Serializable]
    public class EndingsData
    {
        public List<EndingType> PassedEndings = new List<EndingType>();
    }
}