using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Transform>().
            SetParent(eventData.pointerDrag.GetComponent<DragDrop>().parentTransform);
        eventData.pointerDrag.GetComponent<Transform>().SetSiblingIndex(0);
        if (this.name == "DropArea")
        {
            if (eventData.pointerDrag.GetComponent<Transform>().name == "CraftSlot")
            {
                playerCollider.instance.DropItem(true, false);
            }
            else
            {
                playerCollider.instance.DropItem(true, true);
            }
        }
        else if (this.name == "EatArea")
        {
            if (eventData.pointerDrag.GetComponent<Transform>().name == "CraftSlot")
            {
                UiController.instance.EatButtonClicked(false);
            }
            else
            {
                UiController.instance.EatButtonClicked(true);
            }
        }
        else if (this.name == "CraftSlot")
        {
            if (eventData.pointerDrag.GetComponent<Transform>().name == "itemImage")
            {
                //GetComponent<RawImage>().texture = eventData.pointerDrag.GetComponent<RawImage>().texture;
                UiController.instance.AddItemsInCraft();
            }
        }
        else if (this.name == "itemImage")
        {
            if (eventData.pointerDrag.GetComponent<Transform>().name == "CraftSlot")
            {
                //GetComponent<RawImage>().texture = eventData.pointerDrag.GetComponent<RawImage>().texture;
                UiController.instance.RemoveItemsFromCraft();
            }
        }
    }

}
