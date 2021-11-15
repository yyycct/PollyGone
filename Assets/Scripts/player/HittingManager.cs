using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingManager : MonoBehaviour
{
    public static HittingManager instance;
    private void Awake()
    {
        instance = this;
    }
    private Collider col;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("palmTree"))
        {
            col = other;
        }
        else if (other.CompareTag("tree"))
        {
            col = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        col = null;
    }

    public void CutTree()
    {
        if (col != null)
        {
            if (col.CompareTag("palmTree"))
            {
                col.GetComponent<PalmTree>().CutTree();
            }
            else if (col.CompareTag("tree"))
            {
                col.GetComponent<PineTree>().CutTree();
            }
            //col.GetComponent<PalmTree>().CheckState();
        }
    }

}
