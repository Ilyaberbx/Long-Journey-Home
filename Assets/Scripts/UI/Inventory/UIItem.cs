using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class UIItem : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _mainCanvas = GetComponentInParent<Canvas>();
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData) 
            => _rectTransform.anchoredPosition += CalculateDelta(eventData);

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.DOLocalMove(Vector3.zero, 0.5f);
            _canvasGroup.blocksRaycasts = true;
        }

        private Vector2 CalculateDelta(PointerEventData eventData) 
            => eventData.delta / _mainCanvas.scaleFactor;
    }
}