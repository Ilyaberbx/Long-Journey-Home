using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class EnvelopeData
    {
        [SerializeField] private string _envelopeText;

        public string EnvelopeText => _envelopeText;
    }
}