using Logic.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIInventoryItem : UIItem
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _textAmount;
        
        public IInventoryItem Item { get; private set; }

        public void Refresh(IInventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                Clean();
                return;
            }

            Item = slot.Item;
            _icon.sprite = Item.Info.Icon;
            Enable();
            var isCountable = HasAmount(slot);
            
            _textAmount.gameObject.SetActive(isCountable);

            if (isCountable)
                _textAmount.text = $"x{slot.Item.State.Amount.ToString()}";
        }

        private bool HasAmount(IInventorySlot slot) 
            => slot.Amount > 1;

        private void Enable()
        {
            _textAmount.gameObject.SetActive(true);
            _icon.gameObject.SetActive(true);
        }

        private void Clean()
        {
            _textAmount.gameObject.SetActive(false);
            _icon.gameObject.SetActive(false);
        }
    }
}