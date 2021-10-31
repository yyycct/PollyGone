using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineTree : MonoBehaviour
{
    public int health;
    private Animator anim;
    public bool cutable;
    public ParticleSystem TreeFallEFX;
    public ParticleSystem TreeHitEFX;
    public ParticleSystem TreeHitSmokeEFX;
    public items.ItemType woodSpawns;
    public int spawnAmount;
    public GameObject WarnText;
    private AudioSource audio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        WarnText.SetActive(false);
    }
    public void CutTree()
    {
        if (UiController.instance.equipItem.itemType == items.ItemType.Axe)
        {
            health--;
        }

        TreeHitEFX.Play();
        TreeHitSmokeEFX.Play();
        audio.Play();
        CheckState();
    }

    public void CheckState()
    {     
        if (health == 0)
        {
            if (cutable)
            {
                StartCoroutine(TreeFall());
            }
            else
            {
                StartCoroutine(TreeWarn());
                health = 6;
            }
        }
        //anim.Play("treeShake");
    }

    public IEnumerator TreeFall()
    {
        TreeFallEFX.Play();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < spawnAmount; i++)
        {
            GameObject newWoodBlock = Instantiate(items.Get3dGameObject(woodSpawns), this.transform.position, this.transform.rotation);
            newWoodBlock.transform.SetParent(this.transform.parent);
        } 
        //GetComponent<MeshCollider>().enabled = false;
        CollectFeedback.instance.AddtoTree(1);
        Destroy(this.gameObject);
    }
    public IEnumerator TreeWarn()
    {
        WarnText.SetActive(true);
        yield return new WaitForSeconds(2f);
        WarnText.SetActive(false) ;
    }
}
