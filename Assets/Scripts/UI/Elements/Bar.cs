using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(float current, float max) 
            => _image.fillAmount = current / max;
    }
     
}