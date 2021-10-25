using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FireAnimation : MonoBehaviour
{
    //public Animator FireFX;
    private ParticleSystem ps;
    private Light FireLight;
    private TMP_Text FireText;
    bool settingUpFire = false;
    public float settingTime = 2f;
    float timePortion = 0f;
    public bool onFire = false;
    public bool used = false;
    public static FireAnimation instance;
    public float FireTime = 0;
    public float TargetFireSize = 0;
    public float WoodBranchTime = 30f;
    public float WoodBlockTime = 60f;
    public bool UnderCover = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        FireLight = this.transform.GetChild(1).GetComponent<Light>();
        FireText = this.transform.GetChild(2).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        FireTime -= Time.deltaTime;
        if (FireTime < 0) FireTime = 0f;
        if (FireTime == 0f && used)
        {
            playerCollider.instance.wrapUp();
            Destroy(gameObject);
        }
        if (DayControl.instance.raining && !UnderCover)
        {
            FireTime = 2f;
        }
        UpdateFireText();
        if (FireTime > settingTime)
        {
            timePortion += Time.deltaTime;
            if (timePortion >= 2f) timePortion = 2f;
            ps.startSize = Mathf.Lerp(0f, 0.3f, timePortion/settingTime);
            ps.maxParticles = (int)Mathf.Lerp(0f, 50f, timePortion / settingTime);
            FireLight.intensity = Mathf.Lerp(0f, 2f, timePortion / settingTime);
            if (ps.maxParticles > 45)
            {
                ps.maxParticles = 50;
                ps.startSize = 0.3f;
                settingUpFire = false;
                onFire = true;
                //UiController.instance.insText.text = "(F) Cook";
                //UiController.instance.insTwoText.text = "(R) Add Wood Branch";
                //PlayerHealth.instance.nearHeat = true;
                //playerCollider.instance.inteCode = -1;
                //playerCollider.instance.interactable = false;
            }
        }
        else if (FireTime < settingTime)
        {
            timePortion -= Time.deltaTime;
            if (timePortion <= 0f) timePortion = 0f;
            ps.startSize = Mathf.Lerp(0f, 0.3f, timePortion / settingTime);
            ps.maxParticles = (int)Mathf.Lerp(0f, 50f, timePortion / settingTime);
            FireLight.intensity = Mathf.Lerp(0f, 2f, timePortion / settingTime);
            if (ps.maxParticles < 5)
            {
                ps.maxParticles = 0;
                ps.startSize = 0f;
                onFire = false;
                //UiController.instance.insText.text = "(F) Start Fire";
                //UiController.instance.insTwoText.text = "(R) Add Wood Branch";
                //PlayerHealth.instance.nearHeat = false;
                //playerCollider.instance.inteCode = 2;
                //playerCollider.instance.interactable = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RainFreeZone")
        {
            UnderCover = true;
        }
        if (other.tag == "cave")
        {
            UnderCover = true;
        }
    }
    public void StartFireAnim()
    {
        //ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        if (!DayControl.instance.raining)
        {
            FireTime += 60f;
            used = true;
        }
        else if(DayControl.instance.raining && UnderCover)
        {
            FireTime += 60f;
            used = true;
        }
        else
        {
            UiController.instance.changeInsText("It's raining, can't start a fire");
        }
        //Debug.Log("Starting Fire");
    }
    public void UpdateFireText()
    {
        int min = Mathf.FloorToInt(FireTime / 60f);
        int sec = Mathf.FloorToInt(FireTime % 60f);
        FireText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
    public void UpdateFireTime(items.ItemType type)
    {
        switch (type)
        {
            case items.ItemType.Wood:
                FireTime += WoodBranchTime;
                break;
            case items.ItemType.WoodBlock:
                FireTime += WoodBlockTime;
                break;
            default:
                break;
        }
    }
}
