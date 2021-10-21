using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetCrafftItemNum : MonoBehaviour
{
    public void OnItemSelected()
    {
        UiController.instance.craftItemSelected = Int32.Parse(this.name);
        UiController.instance.RemoveItemsFromCraft();
    }
}
