using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetItems : MonoBehaviour
{
    public static PresetItems instance;
    private void Awake()
    {
        instance = this;
    }

    public items rock = new items(items.ItemType.Rock, false, true, true, 0);
    public items mushroom = new items(items.ItemType.Mushroom, true, false, false, 0);
    public items purpleMush = new items(items.ItemType.PurpleMush, true, false, false, 0);
    public items redMush = new items(items.ItemType.RedMush, true, false, false, 0);
    public items wood = new items(items.ItemType.Wood, false, true, false, 0);
    public items cup = new items(items.ItemType.Cup, false, false, false, 0);
    public items can = new items(items.ItemType.Can, true, false, false, 0);
    public items pic = new items(items.ItemType.Pic, false, true, false, 0);
    public items candle = new items(items.ItemType.Candle, false, true, false, 0);
    public items pot = new items(items.ItemType.Pot, false, false, false, 0);
    public items gamCon = new items(items.ItemType.GamCon, false, false, false, 0);
    public items bat = new items(items.ItemType.Bat, false, false, false, 0);
    public items vase = new items(items.ItemType.Vase, false, false, false, 0);
    public items book = new items(items.ItemType.Book, false, true, false, 0);
    public items knife = new items(items.ItemType.Knife, false, true, false, 0);
    public items campFire = new items(items.ItemType.CampFire, false, false, false, 0);
    public items cookedMush = new items(items.ItemType.CookedMush, true, true, false, 0);
    public items axe = new items(items.ItemType.Axe, false, false, true, 0);
    public items woodBlock = new items(items.ItemType.WoodBlock, false, true, false, 0);
    public items water = new items(items.ItemType.Water, true, true, false, 0);
    public items coconut = new items(items.ItemType.Coconut, true, true, false, 0);
    public items GetItemFromType(items.ItemType type)
    {
        switch (type)
        {
            case items.ItemType.Rock:
                return rock;
            case items.ItemType.Mushroom:
                return mushroom;
            case items.ItemType.Wood:
                return wood;
            case items.ItemType.Knife:
                return knife;
            case items.ItemType.Cup:
                return cup;
            case items.ItemType.Pot:
                return pot;
            case items.ItemType.Pic:
                return pic;
            case items.ItemType.Can:
                return can;
            case items.ItemType.GamCon:
                return gamCon;
            case items.ItemType.Candle:
                return candle;
            case items.ItemType.Book:
                return book;
            case items.ItemType.Vase:
                return vase;
            case items.ItemType.Bat:
                return bat;
            case items.ItemType.CampFire:
                return campFire;
            case items.ItemType.CookedMush:
                return cookedMush;
            case items.ItemType.Axe:
                return axe;
            case items.ItemType.WoodBlock:
                return woodBlock;
            case items.ItemType.Water:
                return water;
            case items.ItemType.Coconut:
                return coconut;
            case items.ItemType.Empty:
                return null;
            default:
                break;
        }
        return null;
    }
}
