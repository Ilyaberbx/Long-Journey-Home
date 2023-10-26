using Infrastructure.Services.Hint;
using Logic.Level;
using UnityEngine;
using Zenject;

namespace Logic.Hint
{
    public class HintAction : MonoBehaviour, IAction
    {
        [SerializeField] private string _hintValue;
        private IHintService _hintService;

        [Inject]
        public void Construct(IHintService hintService) 
            => _hintService = hintService;

        public void Execute() 
            => _hintService.ShowHint(_hintValue);
    }
}