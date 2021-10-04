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
    private List<int> idList = new List<int>();
    private int cont =  0;
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
        /*for (int i = 0; i < boxItems.Length; i++)
        {
            idList.Add(i);
        }
        Debug.Log(idList);

        while (idList.Count > 0)
        {
            int rand = Random.Range(0, idList.Count);
            itemId.Add(idList[rand]);
            idList.RemoveAt(idList[rand]);
        }*/
        Debug.Log(itemId);
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
