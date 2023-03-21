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
            item.localPosition = Vector3.zero;
        }
    }
}