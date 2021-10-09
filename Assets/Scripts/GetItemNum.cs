using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetItemNum : MonoBehaviour
{
    public void OnItemSelected()
    {
        UiController.instance.itemSelected = Int32.Parse(this.name);

        if (UiController.instance.cookMode)
        {
            playerCollider.instance.DropCook();
        }

        
    }

    public void OnItemDeselected()
    {
        if (UiController.instance.itemSelected == Int32.Parse(this.name))
        {
            UiController.instance.itemSelected = -1;
        }

    }
}
