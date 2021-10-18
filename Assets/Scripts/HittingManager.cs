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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("palmTree"))
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
        if (UiController.instance.equipItem.itemType == items.ItemType.Axe)
        {
            if (col != null)
            {
                col.GetComponent<PalmTree>().CutTree();
                col.GetComponent<PalmTree>().CheckState();
            }
        }
    }

}
