using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.DialogueSystem
{
    [Serializable]
    public class Dialogue
    {
        [TextArea(1,30)]
        [SerializeField] private List<string> _sentences;
        public List<string> Sentences => _sentences;
    }
}