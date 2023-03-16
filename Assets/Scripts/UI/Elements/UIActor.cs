using Logic.Player;
using UnityEngine;

namespace UI.Elements
{
    public class UIActor : MonoBehaviour
    {
        [SerializeField] private Bar _hpBar;
        private IHealth _health;
        
        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHpBar;
        }

        protected virtual void OnDestroy()
        {
            if (_health != null)
                _health.OnHealthChanged -= UpdateHpBar;
        }
        
        private void UpdateHpBar() 
            => _hpBar.SetValue(_health.CurrentHealth, _health.MaxHp);
    }
}