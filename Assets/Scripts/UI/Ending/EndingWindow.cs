using Infrastructure.StateMachine;
using TMPro;
using UI.Elements;
using UnityEngine;

namespace UI.Ending
{
    public class EndingWindow : WindowBase
    {
        [SerializeField] private string _happyEndText;
        [SerializeField] private string _badEndText;
        [SerializeField] private TextMeshProUGUI _endingText;
        private IGameStateMachine _stateMachine;
        
        public void UpdateUI(EndingType endingType) 
            => _endingText.text = endingType == EndingType.HappyEnd ? _happyEndText : _badEndText;

    
    }
}