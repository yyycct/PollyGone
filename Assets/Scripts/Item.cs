using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public bool ediable;
    public bool combinable;
    public int amount;
    public Texture icon;
    public GameObject model;


}
