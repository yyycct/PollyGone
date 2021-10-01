using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DAssets : MonoBehaviour
{
    public static Item3DAssets instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject Rock;
    public GameObject Mushroom;
    public GameObject Wood;
    public GameObject Knife;
    public GameObject Cup;
    public GameObject Pot;
    public GameObject Pic;
    public GameObject Can;
    public GameObject GamCon;
    public GameObject Candle;
    public GameObject Book;
    public GameObject Vase;
    public GameObject Bat;
    public GameObject CampFire;
    public GameObject PurpleMush;
    public GameObject RedMush;

}
