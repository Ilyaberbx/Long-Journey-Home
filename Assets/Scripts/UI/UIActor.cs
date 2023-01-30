using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIActor : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHpBar;
        }
        
        private void OnDestroy()
        {
            if (_health != null)
                _health.OnHealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHp);
        }
    }
}