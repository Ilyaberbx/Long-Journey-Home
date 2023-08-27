using UI.Elements;
using UnityEngine;

namespace UI.Menu
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private LoadLastSaveButton _lastSaveButton;

        protected override void Initialize()
        {
            if (FirstLoad())
                _lastSaveButton.gameObject.SetActive(false);
        }

        private bool FirstLoad()
            => _progress.IsFirstLoad;
    }
}