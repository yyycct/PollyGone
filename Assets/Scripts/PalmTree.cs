using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTree : MonoBehaviour
{
    private int health = 6;
    private Animator anim;
    public ParticleSystem TreeFallEFX;
    public ParticleSystem TreeHitEFX;
    public ParticleSystem TreeHitSmokeEFX;
    public GameObject palmTree;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void CutTree()
    {
        health--;
        TreeHitEFX.Play();
        TreeHitSmokeEFX.Play();
    }

    public void CheckState()
    {
        if (health == 3)
        {
            DropCoconut();
        }
        else if (health == 0)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine(TreeFall());
        }
        anim.Play("treeShake");
    }

    public void DropCoconut()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (palmTree.transform.GetChild(i).CompareTag("coconut"))
            {
                Transform temp = palmTree.transform.GetChild(i).transform;
                GameObject gob = palmTree.transform.GetChild(i).gameObject;
                gob.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gob.transform.parent = this.transform.parent;
                gob.transform.position = temp.position;
                gob.transform.rotation = temp.rotation;
                
            }

        }
    }

    public IEnumerator TreeFall()
    {
        TreeFallEFX.Play();
        yield return new WaitForSeconds(2f);
        GameObject newWoodBlock = Instantiate(items.Get3dGameObject(items.ItemType.WoodBlock), this.transform.position, this.transform.rotation);
        newWoodBlock.transform.parent = this.transform;
        transform.GetChild(1).SetParent(newWoodBlock.transform);
        GetComponent<MeshCollider>().enabled = false;
        Destroy(palmTree);
    }
}
