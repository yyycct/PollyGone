using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Security.Cryptography;

public class BoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BoxManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] boxItems;
    public int[] itemId;
    void Start()
    {
        itemId = new int[boxItems.Length];
        for (int i = 0; i < boxItems.Length; i++)
        {
            itemId[i] = i;
        }

        System.Random random = new System.Random();
        itemId = itemId.OrderBy(x => random.Next()).ToArray();
        /*while (itemId.Count < boxItems.Length)
        {
            int rand = Random.Range(0, boxItems.Length-1);
            if (!itemId.Contains(rand))
            {
                itemId.Add(rand);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getItem(int i)
    {
        return boxItems[itemId[i]];
    }
}
