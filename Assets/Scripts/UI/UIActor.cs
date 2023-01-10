using System;
using ProjectSolitude.Logic;
using UnityEngine;

namespace UI
{
    public class UIActor : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private HeroHealth _health;

        public void Construct(HeroHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
            => _health.OnHealthChanged -= UpdateHpBar;

        private void UpdateHpBar(float value)
            => _hpBar.SetValue(value, _health.MaxHp);
    }
}