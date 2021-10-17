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
            else if (eventData.pointerDrag.GetComponent<Transform>().name == "itemImage")
            {
                playerCollider.instance.DropItem(true, true);
            }
            else if (eventData.pointerDrag.GetComponent<Transform>().name == "EquipImage")
            {
                PutAxeBackToBag();
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
            else if (eventData.pointerDrag.GetComponent<Transform>().name == "EquipImage")
            {
                PutAxeBackToBag();
            }
        }
        else if (this.name == "EquipArea")
        {
            items temp = null;
            if (eventData.pointerDrag.GetComponent<RawImage>().texture == items.Get2dTexture(items.ItemType.Axe))
            {
                temp = PresetItems.instance.axe;
            }
            else if (eventData.pointerDrag.GetComponent<RawImage>().texture == items.Get2dTexture(items.ItemType.Rock))
            {
                temp = PresetItems.instance.rock;
            }
            EquipTool(temp);
        }
    }

    public void PutAxeBackToBag()
    {
        playerCollider.instance.playerBag.AddItem(PresetItems.instance.axe);
        UnEquipTool();
    }

    public void EquipTool(items item)
    {
        playerCollider.instance.equipped = true;
        if (item.itemType == items.ItemType.Axe)
        {
            UiController.instance.uiviewAxe.SetActive(true);
            playerCollider.instance.axeInHand.SetActive(true);
        }
        else if (item.itemType == items.ItemType.Rock)
        {

        }
        this.transform.GetChild(0).GetComponent<RawImage>().texture = items.Get2dTexture(item.itemType);
        this.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
        this.transform.GetChild(1).gameObject.SetActive(false);
        for (int i = 0; i < playerCollider.instance.playerBag.AllItem.Count; i++)
        {
            if (playerCollider.instance.playerBag.AllItem[i].itemType == item.itemType)
            {
                playerCollider.instance.playerBag.AllItem[i].amount--;
                if (playerCollider.instance.playerBag.AllItem[i].amount <=0)
                {
                    playerCollider.instance.playerBag.AllItem.Remove(playerCollider.instance.playerBag.AllItem[i]);
                }
            }
        }
    }

    public void UnEquipTool()
    {
        playerCollider.instance.equipped = false;
        UiController.instance.uiviewAxe.SetActive(false);
        playerCollider.instance.axeInHand.SetActive(false);
        this.transform.GetChild(0).GetComponent<RawImage>().texture = null;
        this.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }
}
