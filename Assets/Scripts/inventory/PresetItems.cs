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

    public items rock = new items(items.ItemType.Rock);
    public items mushroom = new items(items.ItemType.Mushroom);
    public items purpleMush = new items(items.ItemType.PurpleMush);
    public items redMush = new items(items.ItemType.RedMush);
    public items wood = new items(items.ItemType.Wood);
    public items cup = new items(items.ItemType.Cup);
    public items can = new items(items.ItemType.Can);
    public items pic = new items(items.ItemType.Pic);
    public items candle = new items(items.ItemType.Candle);
    public items pot = new items(items.ItemType.Pot);
    public items gamCon = new items(items.ItemType.GamCon);
    public items bat = new items(items.ItemType.Bat);
    public items vase = new items(items.ItemType.Vase);
    public items book = new items(items.ItemType.Book);
    public items skateShoes = new items(items.ItemType.SkateShoe);
    public items campFire = new items(items.ItemType.CampFire);
    public items cookedMush = new items(items.ItemType.CookedMush);
    public items axe = new items(items.ItemType.Axe);
    public items woodBlock = new items(items.ItemType.WoodBlock);
    public items plank = new items(items.ItemType.Plank);
    public items water = new items(items.ItemType.Water);
    public items coconut = new items(items.ItemType.Coconut);
    public items soup = new items(items.ItemType.Soup);
    public items boat = new items(items.ItemType.Boat);
    public items GetItemFromType(items.ItemType type)
    {
        switch (type)
        {
            case items.ItemType.Rock:
                return rock;
            case items.ItemType.Mushroom:
                return mushroom;
            case items.ItemType.PurpleMush:
                return purpleMush;
            case items.ItemType.RedMush:
                return redMush;
            case items.ItemType.Wood:
                return wood;
            case items.ItemType.SkateShoe:
                return skateShoes;
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
            case items.ItemType.Plank:
                return plank;
            case items.ItemType.Water:
                return water;
            case items.ItemType.Coconut:
                return coconut;
            case items.ItemType.Soup:
                return soup;
            case items.ItemType.Boat:
                return boat;
            case items.ItemType.Empty:
                return null;
            default:
                break;
        }
        return null;
    }

}
