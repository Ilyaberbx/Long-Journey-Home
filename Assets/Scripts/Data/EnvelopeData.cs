using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class EnvelopeData
    {
        [SerializeField] private string _content;

        public string Content => _content;
    }
}