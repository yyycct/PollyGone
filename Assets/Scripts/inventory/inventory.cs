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

    public bool AddItem(items _item)
    {
        bool repeat = false;
        bool added = false;
        for (int i = 0; i < AllItem.Count; i++)
        {
            if (AllItem[i] == _item)
            {
                if (_item.combinable)
                {
                    repeat = true;
                    AllItem[i].amount++;
                    added = true;
                    break;
                }
                else
                {
                    repeat = false;
                    break;
                }
            }
            UiController.instance.lastInBagNumber = i;
        }
        if (!repeat)
        {
            if (AllItem.Count >= bagCapacity)
            {
                bagFull = true;
                Tutorial.instance.BagIsFull();
            }
            else
            {
                bagFull = false;
                AllItem.Add(_item);
                added = true;
                _item.amount = 1;
                UiController.instance.lastInBagNumber = AllItem.Count-1;
            }
        }
        return added;
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
