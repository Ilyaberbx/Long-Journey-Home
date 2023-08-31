using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake() 
            => _button.onClick.AddListener(Execute);
        
        private void OnDestroy() 
            => _button.onClick.RemoveListener(Execute);
        
        protected abstract void Execute();
    }
}