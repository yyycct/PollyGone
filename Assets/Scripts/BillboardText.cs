using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardText : MonoBehaviour
{
    //public GameObject cameraRotation;
    // Start is called before the first frame update
    GameObject cameraRotaion;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        cameraRotaion = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(cameraRotaion.transform.eulerAngles.x, cameraRotaion.transform.eulerAngles.y, 0f);
    }
}
