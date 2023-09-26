using UI.Elements;
using UnityEngine;

namespace UI.Menu
{

    public class ExitButton : BaseButton
    {
        protected override void Execute()
        {
            DisableButton();
            Application.Quit();
        }
    }
}