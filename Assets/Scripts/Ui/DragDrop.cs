using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DragDrop : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas canvas;
    private Transform topLayerTransform;

    private bool isObject = false;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public Transform parentTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Start()
    {
        parentTransform = this.transform.parent;
        canvas = UiController.instance.canvas;
        topLayerTransform = UiController.instance.topLayerTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        int selection = -1;
        if (this.name != "EquipImage")
        {
            selection = int.Parse(this.transform.parent.name);
        }

        if (this.name == "itemImage")
        {
            UiController.instance.DragItemNumber = selection;
            if (playerCollider.instance.playerBag.GetItem(selection).ediable)
            {
                UiController.instance.eatArea.SetActive(true);
            }
            if (playerCollider.instance.playerBag.GetItem(selection).usable)
            {
                UiController.instance.equipArea.SetActive(true);
            }
            Tutorial.instance.UseItemTuto(playerCollider.instance.playerBag.GetItem(selection));
        }
        else if (this.name == "CraftSlot")
        {
            UiController.instance.craftItemSelected = selection;
            if (UiController.instance.itemsInCraft[selection].ediable)
            {
                UiController.instance.eatArea.SetActive(true);
            }
            if (UiController.instance.itemsInCraft[selection].usable)
            {
                UiController.instance.equipArea.SetActive(true);
            }
        }

        if (this.name == "EquipImage")
        {
            UiController.instance.DraggingTool = true;
        }
        else
        {
            UiController.instance.DraggingTool = false;
        }


        if (GetComponent<RawImage>().texture == null ||
            GetComponent<RawImage>().texture == UiController.instance.EmptySprite)
        {
            isObject = false;
        }
        else
        {
            isObject = true;
        }

        UiController.instance.dropArea.SetActive(true);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;

        this.transform.parent = topLayerTransform;
        UiController.instance.isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isObject)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        UiController.instance.isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UiController.instance.isDragging = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        this.transform.SetParent(parentTransform);
        this.transform.SetSiblingIndex(0);
        UiController.instance.dropArea.SetActive(false);
        UiController.instance.eatArea.SetActive(false);
        
        playerCollider.instance.loopInventory();
        UiController.instance.PrintCrafts();
        /*Debug.Log("bag: " + playerCollider.instance.playerBag.AllItem.Count.ToString());
        Debug.Log("craft: " + UiController.instance.itemsInCraft.Count.ToString());*/

        if (!playerCollider.instance.equipped)
        {
            UiController.instance.equipArea.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(this.GetComponentInParent<Transform>().name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!UiController.instance.isDragging)
        {
            int selection = int.Parse(this.transform.parent.name);
            items item = null;
            if (this.name == "itemImage")
            {
                if (playerCollider.instance.playerBag.GetSize() >= selection + 1)
                {
                    item = playerCollider.instance.playerBag.GetItem(selection);
                }
            }
            else if (this.name == "CraftSlot")
            {
                if (UiController.instance.itemsInCraft.Count >= selection + 1)
                {
                    item = UiController.instance.itemsInCraft[selection];
                }
            }
            else if (this.name == "EquipImage")
            {
                item = UiController.instance.equipItem;
            }
            ItemDescriptionController.instance.ShowDescription(item, this.rectTransform);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescriptionController.instance.HideDescription();
    }
}