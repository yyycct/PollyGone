using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DAssets : MonoBehaviour
{
    public static Item2DAssets instance;
    private void Awake()
    {
        instance = this;
    }

    public Texture Rock;
    public Texture Mushroom;
    public Texture Wood;
    public Texture Knife;
    public Texture Cup;
    public Texture Pot;
    public Texture Pic;
    public Texture Can;
    public Texture GamCon;
    public Texture Candle;
    public Texture Book;
    public Texture Vase;
    public Texture Bat;
    public Texture CampFire;
    public Texture Empt;
    public Texture PurpleMush;
    public Texture RedMush;
    public Texture CookedMush;
    public Texture Axe;

}
