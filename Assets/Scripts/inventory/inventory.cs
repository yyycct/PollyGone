using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory
{
    public List<items> AllItem;
    public bool bagFull;
    public int bagCapacity = 12;
    public inventory()
    {
        AllItem = new List<items>();
    }

    public void AddItem(items _item)
    {
        bool repeat = false;
        for (int i = 0; i < AllItem.Count; i++)
        {
            if (AllItem[i] == _item)
            {
                repeat = true;
                AllItem[i].amount++;
                continue;
            }
            UiController.instance.lastInBagNumber = i;
        }
        if (!repeat)
        {
            if (AllItem.Count >= bagCapacity)
            {
                bagFull = true;
                UiController.instance.insText.text = "Bag is full";
            }
            else
            {
                bagFull = false;
                AllItem.Add(_item);
                _item.amount++;
                UiController.instance.lastInBagNumber = AllItem.Count-1;
            }
        }
    }
    public items GetItem(int i)
    {
        return AllItem[i];
    }

    public int GetSize()
    {
        return AllItem.Count;
    }
}
