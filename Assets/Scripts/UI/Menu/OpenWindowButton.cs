using UI.Elements;
using UI.Services.Window;
using UnityEngine;

namespace UI.Menu
{
    public class OpenWindowButton : BaseButton
    {
        [SerializeField] private WindowType _windowType;
        private IWindowService _windowService;
        public void Construct(IWindowService windowService) 
            => _windowService = windowService;

        public override void Execute() 
            => _windowService.Open(_windowType);
    }
}