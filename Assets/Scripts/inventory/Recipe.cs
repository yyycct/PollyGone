using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<items.ItemType> craftMaterials;
    public items.ItemType craftResult;
    public bool isCookRecipe;

    public string GetMaterialName(int index)
    {
        return PresetItems.instance.GetItemFromType(craftMaterials[index]).itemName;
    }

    public string GetResultName()
    {
        return PresetItems.instance.GetItemFromType(craftResult).itemName;
    }
}
