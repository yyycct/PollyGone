using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items
{
    public enum ItemType
    {
        Rock,
        Mushroom,
        Wood,
        Knife,
        Cup,
        Pot,
        Pic,
        Can,
        GamCon,
        Candle,
        Book,
        Vase,
        Bat,
        CampFire,
        PurpleMush,
        RedMush,
        CookedMush,
        Axe,
        WoodBlock,
        Water,
        Coconut,
        Empty
    }

    public ItemType itemType;
    public bool ediable;
    public bool combinable;
    //public int index;
    public bool usable;
    public int amount;
    public items(ItemType _itemType, bool _ediable, bool _combinable, bool _usable, int _amount)
    {
        itemType = _itemType;
        ediable = _ediable;
        combinable = _combinable;
        usable = _usable;
        amount = _amount;
    }

    public items(ItemType _itemType)
    {
        switch (_itemType)
        {
            case ItemType.Rock:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = true;
                amount = 0;
                break;
            case ItemType.Mushroom:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            case ItemType.Wood:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            case ItemType.Knife:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Cup:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Pot:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Pic:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Can:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.GamCon:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Candle:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Book:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Vase:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.Bat:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.CampFire:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                break;
            case ItemType.CookedMush:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            case ItemType.Axe:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = true;
                amount = 0;
                break;
            case ItemType.WoodBlock:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            case ItemType.Water:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            case ItemType.Coconut:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                break;
            default:
                break;
        }
    }

    
    public Texture Get2dTexture()
    {
        return Get2dTexture(itemType);
    }
    public static Texture Get2dTexture(items.ItemType _itemType)
    {
        var ob = Item2DAssets.instance;
        switch (_itemType)
        {
            case ItemType.Rock:
                return ob.Rock;
            case ItemType.Mushroom:
                return ob.Mushroom;
            case ItemType.PurpleMush:
                return ob.PurpleMush;
            case ItemType.RedMush:
                return ob.RedMush;
            case ItemType.Wood:
                return ob.Wood;
            case ItemType.Knife:
                return ob.Knife;
            case ItemType.Cup:
                return ob.Cup;
            case ItemType.Pot:
                return ob.Pot;
            case ItemType.Pic:
                return ob.Pic;
            case ItemType.Can:
                return ob.Can;
            case ItemType.GamCon:
                return ob.GamCon;
            case ItemType.Candle:
                return ob.Candle;
            case ItemType.Book:
                return ob.Book;
            case ItemType.Vase:
                return ob.Vase;
            case ItemType.Bat:
                return ob.Bat;
            case ItemType.CampFire:
                return ob.CampFire;
            case ItemType.CookedMush:
                return ob.CookedMush;
            case ItemType.Axe:
                return ob.Axe;
            case ItemType.WoodBlock:
                return ob.WoodBlock;
            case ItemType.Water:
                return ob.Water;
            case ItemType.Coconut:
                return ob.Coconut;
            case ItemType.Empty:
                return ob.Empt;
            default:
                break;
        }
        return null;
    }
    public GameObject Get3dGameObject()
    {
        return Get3dGameObject(itemType);
    }
    public static GameObject Get3dGameObject(items.ItemType _itemType)
    {
        var ob = Item3DAssets.instance;
        switch (_itemType)
        {
            case ItemType.Rock:
                return ob.Rock;
            case ItemType.Mushroom:
                return ob.Mushroom;
            case ItemType.PurpleMush:
                return ob.PurpleMush;
            case ItemType.RedMush:
                return ob.RedMush;
            case ItemType.Wood:
                return ob.Wood;
            case ItemType.Knife:
                return ob.Knife;
            case ItemType.Cup:
                return ob.Cup;
            case ItemType.Pot:
                return ob.Pot;
            case ItemType.Pic:
                return ob.Pic;
            case ItemType.Can:
                return ob.Can;
            case ItemType.GamCon:
                return ob.GamCon;
            case ItemType.Candle:
                return ob.Candle;
            case ItemType.Book:
                return ob.Book;
            case ItemType.Vase:
                return ob.Vase;
            case ItemType.Bat:
                return ob.Bat;
            case ItemType.CampFire:
                return ob.CampFire;
            case ItemType.CookedMush:
                return ob.CookedMush;
            case ItemType.Axe:
                return ob.Axe;
            case ItemType.WoodBlock:
                return ob.WoodBlock;
            case ItemType.Water:
                return ob.Water;
            case ItemType.Coconut:
                return ob.Coconut;
            default:
                break;
        }
        
        return null;
    }
}
