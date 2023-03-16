using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class UISlot : MonoBehaviour,IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.transform;
            item.SetParent(transform);
            item.transform.DOLocalMove(Vector3.zero, 0.5f);
        }
    }
}