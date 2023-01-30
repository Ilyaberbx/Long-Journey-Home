using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpImage;

        public void SetValue(float current, float max) 
            => _hpImage.fillAmount = current / max;
    }
     
}