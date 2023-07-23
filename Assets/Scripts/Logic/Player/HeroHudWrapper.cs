using UI.Elements;
using UnityEngine;

namespace Logic.Player
{
    public class  HeroHudWrapper : MonoBehaviour
    {
        private IHud _hud;

        public void SetHud(IHud hud) 
            => _hud = hud;

        public void Hide() 
            => _hud.Hide();

        public void Open() 
            => _hud.Open();
    }
}