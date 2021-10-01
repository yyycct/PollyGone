using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<items.ItemType> craftMaterials;
    public items.ItemType craftResult;
}
