using UI.Elements;
using UI.Services.Window;
using UnityEngine;
using Zenject;

namespace UI.Menu
{
    public class OpenWindowButton : BaseButton
    {
        [SerializeField] private WindowType _windowType;
        private IWindowService _windowService;

        [Inject]
        public void Construct(IWindowService windowService)
            => _windowService = windowService;

        public override void Execute()
            => _windowService.Open(_windowType);
    }
}