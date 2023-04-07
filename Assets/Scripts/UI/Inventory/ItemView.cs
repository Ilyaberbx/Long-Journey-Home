using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemView : MonoBehaviour
    {
        public event Action<ItemView> OnItemClicked, OnRightMouseClicked;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Image _borderImage;

        private bool _isEmpty;

        private void Awake()
        {
            CleanUp();
            Deselect();
        }
        
        public void OnPointerClick(BaseEventData data)
        {
            Deselect();
            
            PointerEventData pointerData = (PointerEventData)data;
            
            if (pointerData.button == PointerEventData.InputButton.Right)
                OnRightMouseClicked?.Invoke(this);
            else
                OnItemClicked?.Invoke(this);
        }

        public void SetData(Sprite sprite, int amount)
        {
            _image.gameObject.SetActive(true);
            _image.sprite = sprite;
            _amount.text = amount + "";
            _isEmpty = false;
        }

        public void Select() 
            => _borderImage.enabled = true;

        public void Deselect() 
            => _borderImage.enabled = false;

        public void CleanUp()
        {
            _image.gameObject.SetActive(false);
            _isEmpty = true;
        }
        
    }
}