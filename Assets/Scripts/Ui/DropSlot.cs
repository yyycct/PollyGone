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

        items temp = null;
        if (eventData.pointerDrag.GetComponent<RawImage>().texture == items.Get2dTexture(items.ItemType.Axe))
        {
            temp = PresetItems.instance.axe;
        }
        else if (eventData.pointerDrag.GetComponent<RawImage>().texture == items.Get2dTexture(items.ItemType.Rock))
        {
            temp = PresetItems.instance.rock;
        }

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
                UnEquipTool(temp, true);
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
            else if (eventData.pointerDrag.GetComponent<Transform>().name == "EquipImage")
            {
                PutToolBackToBag(temp);
                UiController.instance.DragItemNumber = UiController.instance.lastInBagNumber;
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
                PutToolBackToBag(temp);
            }
        }
        else if (this.name == "EquipArea")
        {
            
            if (eventData.pointerDrag.GetComponent<Transform>().name == "itemImage")
            {
                EquipTool(temp, true);
            }
            else if (eventData.pointerDrag.GetComponent<Transform>().name == "CraftSlot")
            {
                EquipTool(temp, false);
            }
            
        }
    }

    public void PutToolBackToBag(items item)
    {
        playerCollider.instance.playerBag.AddItem(item);
        UnEquipTool(item, false);
        playerCollider.instance.loopInventory();
    }

    public void EquipTool(items item, bool bag)
    {
        if (!playerCollider.instance.equipped)
        {
            playerCollider.instance.equipped = true;
            UiController.instance.equipItem = item;
            if (item.itemType == items.ItemType.Axe)
            {
                UiController.instance.uiviewAxe.SetActive(true);
                playerCollider.instance.axeInHand.SetActive(true);
            }
            else if (item.itemType == items.ItemType.Rock)
            {
                UiController.instance.uiviewRock.SetActive(true);
                playerCollider.instance.rockInHand.SetActive(true);
            }
            UiController.instance.equipArea.transform.GetChild(0).GetComponent<RawImage>().texture = items.Get2dTexture(item.itemType);
            UiController.instance.equipArea.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            UiController.instance.equipArea.transform.GetChild(1).gameObject.SetActive(false);

            playerCollider.instance.DropItem(false, bag);

            UiController.instance.equipArea.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
            UiController.instance.equipArea.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    public void UnEquipTool(items item, bool spawn)
    {
        if (spawn)
        {
            GameObject newItem = Instantiate(items.Get3dGameObject(UiController.instance.equipItem.itemType),
            playerCollider.instance.dropPoint.transform.position, playerCollider.instance.dropPoint.transform.rotation);
        }
        UiController.instance.equipItem = null;
        UiController.instance.equipArea.SetActive(false);
        playerCollider.instance.equipped = false;

        if (item.itemType == items.ItemType.Axe)
        {
            UiController.instance.uiviewAxe.SetActive(false);
            playerCollider.instance.axeInHand.SetActive(false);
        }
        else if (item.itemType == items.ItemType.Rock)
        {
            UiController.instance.uiviewRock.SetActive(false);
            playerCollider.instance.rockInHand.SetActive(false);
        }

        UiController.instance.equipArea.transform.GetChild(0).GetComponent<RawImage>().texture = null;
        UiController.instance.equipArea.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
        UiController.instance.equipArea.transform.GetChild(1).gameObject.SetActive(true);
    }
}
