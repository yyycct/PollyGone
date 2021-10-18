using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWater : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject water;
    public float maxHeight =0.15f;
    public float waterSpeed = 0.05f;
    private float waterTimer;
    void Start()
    {
        water = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        waterTimer += Time.deltaTime * waterSpeed;
        if (DayControl.instance.raining)
        {
            if (water.transform.localScale.y < maxHeight)
            {
              
                float raise = Mathf.Lerp(0f, maxHeight, waterTimer/1);
                water.transform.localScale = new Vector3(water.transform.localScale.x,
                    raise, water.transform.localScale.z);
                if(this.tag == "cup" || this.tag == "pot")
                {
                    water.transform.localPosition = new Vector3(water.transform.localPosition.x,
                    raise, water.transform.localPosition.z);
                }
            }
        }
    }
}
