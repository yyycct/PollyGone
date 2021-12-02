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

    const float maxSizeTime = 600f;
    const float minSizeTime = 60f;
    const float maxSize = 3f;
    const float minSize = 1f;
    private float currentSize = 1f;
    private bool wet = false;
    private ParticleSystem.VelocityOverLifetimeModule VelocityOverLifetimeModule;
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
        if (DayControl.instance.raining && !UnderCover && !wet && used)
        {
            FireTime = 2f;
            wet = true;
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

        FireSizeUpdate();
        if (HelicopterMove.instance.inRoute && FireTime >= maxSizeTime)
        {
            Tutorial.instance.HelicopterEnding();
        }
    }

    private void FireSizeUpdate()
    {
        if (FireTime <= minSizeTime)
        {
            currentSize = minSize;
        }
        else if (FireTime >= maxSizeTime)
        {
            currentSize = maxSize;
            ps.startSize = 0.9f;
            ps.maxParticles = 150;
            VelocityOverLifetimeModule = ps.velocityOverLifetime;
            VelocityOverLifetimeModule.yMultiplier = 1.8f;
            FireLight.range = 6f;
        }
        else
        {
            currentSize = Mathf.Lerp(minSize, maxSize, (FireTime - minSizeTime) / (maxSizeTime - minSizeTime));
            ps.startSize = Mathf.Lerp(0.3f, 0.9f, (FireTime - minSizeTime) / (maxSizeTime - minSizeTime));
            ps.maxParticles = (int)Mathf.Lerp(50f, 150f, (FireTime - minSizeTime) / (maxSizeTime - minSizeTime));
            FireLight.range = Mathf.Lerp(2f, 6f, (FireTime - minSizeTime) / (maxSizeTime - minSizeTime));
            VelocityOverLifetimeModule = ps.velocityOverLifetime;
            VelocityOverLifetimeModule.yMultiplier = Mathf.Lerp(0.6f, 1.8f, (FireTime - minSizeTime) / (maxSizeTime - minSizeTime));
        }

        this.gameObject.transform.localScale = new Vector3(currentSize, currentSize, currentSize);

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
        FireTime += 60f;
        used = true;
        //Debug.Log("Starting Fire");
    }
    public void UpdateFireText()
    {
        int min = Mathf.FloorToInt(FireTime / 60f);
        int sec = Mathf.FloorToInt(FireTime % 60f);
        FireText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
    public void UpdateFireTime(items item)
    {
        FireTime += item.fireTime;
    }
}
