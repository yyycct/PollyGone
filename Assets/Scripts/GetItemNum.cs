using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetItemNum : MonoBehaviour
{
    public void OnItemSelected()
    {
        UiController.instance.itemSelected = Int32.Parse(this.name);
        if (UiController.instance.craftMode)
        {
            UiController.instance.AddItemsInCraft();
        }
        else 
        {
            UiController.instance.dropButton.SetActive(true);
            if (playerCollider.instance.playerBag.GetItem(UiController.instance.itemSelected).ediable)
            {
                UiController.instance.eatButton.SetActive(true);
            }
            else
            {
                UiController.instance.eatButton.SetActive(false);
            }
        }
    }

    public void OnItemDeselected()
    {
        if (UiController.instance.itemSelected == Int32.Parse(this.name))
        {
            UiController.instance.itemSelected = -1;
        }
        UiController.instance.checkDropButtonState();

    }
}
