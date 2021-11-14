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
        SkateShoe,
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
        Plank,
        Water,
        Coconut,
        Soup,
        Boat,
        Empty
    }

    public ItemType itemType;
    public bool ediable = false;
    public bool combinable = true;
    public bool usable = false;
    public int amount = 0;
    public int hungerValuePlus = 0;
    public int hydrateValuePlus = 0;
    public int fireTime = 0;
    public string itemName;
    public string description;
    public items(ItemType _itemType, bool _ediable, bool _combinable, bool _usable, int _amount, 
       int _hungerValuePlus, int _hydrateValuePlus, int _fireTime, string _itemName, string _description)
    {
        itemType = _itemType;
        ediable = _ediable;
        combinable = _combinable;
        usable = _usable;
        amount = _amount;
        hungerValuePlus = _hungerValuePlus;
        hydrateValuePlus = _hydrateValuePlus;
        fireTime = _fireTime;
        itemName = _itemName;
        description = _description;
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
                fireTime = 0;
                itemName = "Small Rock";
                description = "Seems small, but may be used to build something with?";
                break;
            case ItemType.Mushroom:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 10;
                fireTime = 0;
                itemName = "Mushroom";
                description = "Looks muddy, but taste great!";
                break;
            case ItemType.PurpleMush:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 10;
                fireTime = 0;
                itemName = "Mushroom";
                description = "The color of this mushroom looks pretty, but can I eat it?";
                break;
            case ItemType.RedMush:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 10;
                fireTime = 0;
                itemName = "Mushroom";
                description = "The color of this mushroom looks pretty, but can I eat it?";
                break;
            case ItemType.Wood:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = false;
                amount = 0;
                fireTime = 30;
                itemName = "Wood Branch";
                description = "They are all over this island, maybe I could start a fire with them";
                break;
            case ItemType.SkateShoe:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                itemName = "Skate Shoes";
                description = "The blade on these shoes looks really sharp, maybe good for cutting things";
                break;
            case ItemType.Cup:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                itemName = "Small Cup";
                description = "Miss my daily coffee, all I have is salty water now";
                break;
            case ItemType.Pot:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                itemName = "Big Pot";
                description = "Hmm, wonder if I cook with this";
                break;
            case ItemType.Pic:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 20;
                itemName = "Painting";
                description = "This painting looks stunning";

                break;
            case ItemType.Can:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                hungerValuePlus = 20;
                hydrateValuePlus = 20;
                itemName = "Tomato Soup Can";
                description = "Food!!!, this would taste so much better if it was hot";
                break;
            case ItemType.GamCon:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                itemName = "Game Controller";
                description = "It would be great to have on any other days, just not right not...";
                break;
            case ItemType.Candle:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 10;
                itemName = "Candle";
                description = "Can I light this up?";
                break;
            case ItemType.Book:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 40;
                itemName = "Book";
                description = "hmm, maybe read it when I am bored";
                break;
            case ItemType.Vase:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 0;
                itemName = "Flower Vase";
                description = "Not sure what I could put in the vase";
                break;
            case ItemType.Bat:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 20;
                break;
            case ItemType.CampFire:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 60;
                itemName = "Camp Fire";
                description = "The night is freezing here, this will help me through the night, probably should place this somewhere safe";
                break;
            case ItemType.CookedMush:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 20;
                fireTime = 0;
                itemName = "Cooked Mushroom";
                description = "The mushroom taste way better after it's cooked";
                break;
            case ItemType.Axe:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = true;
                amount = 0;
                fireTime = 0;
                itemName = "Axes";
                description = "Barely holding together, but that's all the tool I have here";
                break;
            case ItemType.WoodBlock:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = false;
                amount = 0;
                fireTime = 60;
                itemName = "Wood Block";
                description = "This wood block seems bigger than the wood branch, what can I make with this, or should I burn this instead";
                break;
            case ItemType.Plank:
                itemType = _itemType;
                ediable = false;
                combinable = true;
                usable = false;
                amount = 0;
                fireTime = 180;
                itemName = "Wood Plank";
                description = "I made them with wood block, may be I could create a boat with these";
                break;
            case ItemType.Water:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hydrateValuePlus = 10;
                fireTime = -20;
                itemName = "Rain Water";
                description = "Seems safe to drink";
                break;
            case ItemType.Coconut:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 10;
                hydrateValuePlus = 10;
                fireTime = -10;
                itemName = "Coconut";
                description = "Taste so refreshing";
                break;
            case ItemType.Soup:
                itemType = _itemType;
                ediable = true;
                combinable = true;
                usable = false;
                amount = 0;
                hungerValuePlus = 20;
                hydrateValuePlus = 20;
                fireTime = -20;
                itemName = "Soup";
                description = "The best soup I ever had";
                break;
            case ItemType.Boat:
                itemType = _itemType;
                ediable = false;
                combinable = false;
                usable = false;
                amount = 0;
                fireTime = 400;
                itemName = "Boat";
                description = "A tiny woodern boat made by Polly, look unstable but that's the last hope I have..";
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
            case ItemType.SkateShoe:
                return ob.SkateShoe;
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
            case ItemType.Plank:
                return ob.Plank;
            case ItemType.Water:
                return ob.Water;
            case ItemType.Coconut:
                return ob.Coconut;
            case ItemType.Soup:
                return ob.Soup;
            case ItemType.Boat:
                return ob.Boat;
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
            case ItemType.SkateShoe:
                return ob.SkateShoe;
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
            case ItemType.Plank:
                return ob.Plank;
            case ItemType.Water:
                return ob.Water;
            case ItemType.Coconut:
                return ob.Coconut;
            case ItemType.Soup:
                return ob.Soup;
            case ItemType.Boat:
                return ob.Boat;
            default:
                break;
        }
        
        return null;
    }
}
