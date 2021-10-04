using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BoxManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] boxItems;
    public List<int> itemId = new List<int>();
    void Start()
    {
        while (itemId.Count < boxItems.Length)
        {
            int rand = Random.Range(0, 9);
            if (!itemId.Contains(rand))
            {
                itemId.Add(rand);
            }
        }
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
