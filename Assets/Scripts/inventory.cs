using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory
{
    public List<items> AllItem;
    public inventory()
    {
        AllItem = new List<items>();
    }

    public void AddItem(items _item)
    {
        bool repeat = false;
        if (AllItem.Count < 12)
        {
            for (int i = 0; i < AllItem.Count; i++)
            {
                if (AllItem[i] == _item)
                {
                    repeat = true;
                    AllItem[i].amount++;
                    continue;
                }
            }
            if (!repeat)
            {
                AllItem.Add(_item);
                _item.amount++;
            }
            
        }
        else
        {
            UiController.instance.insText.text = "Bag is Full";
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
