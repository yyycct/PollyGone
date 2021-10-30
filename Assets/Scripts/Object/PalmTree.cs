using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTree : MonoBehaviour
{
    private int health = 6;
    private int coconutHealth = 3;
    private Animator anim;
    public ParticleSystem TreeFallEFX;
    public ParticleSystem TreeHitEFX;
    public ParticleSystem TreeHitSmokeEFX;
    private AudioSource audio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void CutTree()
    {
        if (UiController.instance.equipItem.itemType == items.ItemType.Axe)
        {
            health--;
            coconutHealth--;
        }
        else if (UiController.instance.equipItem.itemType == items.ItemType.Rock)
        {
            coconutHealth--;
        }
        TreeHitEFX.Play();
        TreeHitSmokeEFX.Play();
        audio.Play();
        CheckState();
    }

    public void CheckState()
    {
        if (coconutHealth == 0)
        {
            DropCoconut();
        }
        else if (health == 0)
        {
            StartCoroutine(TreeFall());
        }
        anim.Play("treeShake");
    }

    public void DropCoconut()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(0).CompareTag("coconut"))
            {
                Transform temp = transform.GetChild(i).transform;
                GameObject gob = transform.GetChild(i).gameObject;
                
                gob.transform.SetParent(this.transform.parent.parent);
                gob.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gob.transform.position = temp.position;
                gob.transform.rotation = temp.rotation;
            }
        }
    }

    public IEnumerator TreeFall()
    {
        TreeFallEFX.Play();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(2f);
        GameObject newWoodBlock = Instantiate(items.Get3dGameObject(items.ItemType.WoodBlock), this.transform.position, this.transform.rotation);
        newWoodBlock.transform.SetParent(this.transform.parent);
        //GetComponent<MeshCollider>().enabled = false;
        CollectFeedback.instance.AddtoTree(1);
        Destroy(this.gameObject);
    }
}
