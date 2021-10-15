using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWater : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject water;
    public float maxHeight =0.25f;
    public float waterSpeed = 1f;
    void Start()
    {
        water = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (DayControl.instance.raining)
        {
            if (water.transform.localScale.y < maxHeight)
            {
                water.transform.localScale = new Vector3(water.transform.localScale.x,
                    Mathf.Lerp(0f, maxHeight, Time.deltaTime * waterSpeed),water.transform.localScale.z);
            }
        }
    }
}
