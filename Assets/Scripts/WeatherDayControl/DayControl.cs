using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.WeatherMaker;
public class DayControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float timeofDay;
    [SerializeField] private LayerMask night;
    [SerializeField] private LayerMask day;
    [SerializeField] private Light theSun;
    [SerializeField] private Light MoonLight;
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private float moonIntensity = 0.02f;
    [SerializeField] private Color rainColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color cloudyColor;
    [SerializeField] private Color nightFog;
    [SerializeField] private Color dayFog;
    [SerializeField] private int DayCount = 1;
    [SerializeField] private GameObject rainwaterdemo;
    public float dayLightIntensity = 1f;
    public bool raining;
    public bool cloudy = false;
    public bool nightTime = false;
    [SerializeField] GameObject rainEFX;
    float timeElapsed;
    private int randomRainTime;
    private int rainRemainTime;
    //how many seconds is a day in game
    private float timeRatio = 6 * 60 / 24;
    public static DayControl instance;
    private int boxSpawned = 1;
    public int numOfBoxToSpawn = 5;
    public int numOfMushToSpawn = 15;
    public GameObject helicopater;
    private bool mushSpawned = false;
    private bool heli = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        randomRainTime = Random.Range(11, 15);
        rainRemainTime = Random.Range(3, 6);
        Debug.Log(randomRainTime);
    }

    // Update is called once per frame
    void Update()
    {
        //timeofDay += Time.deltaTime / timeRatio;
        //if (timeofDay >= 24) { DayCount++; }
        //timeofDay %= 24;
        matchWeathmakerDayCycle();
        /*if (timeofDay < 6 || timeofDay > 18)
        {
            //RenderSettings.fog = true;
            //RenderSettings.fogColor = nightFog;
            //RenderSettings.fogEndDistance = 20;
            nightTime = true;
        }
        else
        {
            RenderSettings.fog = false;
            nightTime = false;
        }*/
        if(DayCount == 1 && timeofDay == 16f)
        {
            if (boxSpawned >0)
            {
                SpawnBoxes.instance.spawnBox(numOfBoxToSpawn);
                boxSpawned--;
            }
        }
        else if(DayCount ==1 && timeofDay > 16f)
        {
            boxSpawned = 1;
        }
        else if(DayCount == 2 && timeofDay == 8f)
        {
            raining = true;
            cloudy = false;
            rainwaterdemo.SetActive(true);
            AudioManager.instance.OnRaining();
        }
        else if(DayCount == 2 && timeofDay > 8f && timeofDay < 9f)
        {
            raining = true;
            cloudy = false;
        }
        else if(DayCount == 2 && timeofDay == 9f)
        {
            raining = false;
            cloudy = false;
            AudioManager.instance.OnDayBegin();
        }
        else if(DayCount == 2 && timeofDay == randomRainTime)
        {
            AudioManager.instance.OnRaining();
        }
        else if (DayCount == 2 && timeofDay > randomRainTime && timeofDay < (randomRainTime + rainRemainTime))
        {
            raining = true;
            cloudy = false;
        }
        else if(DayCount == 2 && timeofDay == (randomRainTime + rainRemainTime))
        {
            if (nightTime)
            {
                AudioManager.instance.OnNighBegin();
            }
            else
            {
                AudioManager.instance.OnDayBegin();
            }
        }
        else if (DayCount == 2 && timeofDay > (randomRainTime + rainRemainTime) )
        {
            if (boxSpawned > 0)
            {
                SpawnBoxes.instance.spawnBox(numOfBoxToSpawn);
                boxSpawned --;
            }
            if (!mushSpawned)
            {
                SpawnMushroom.instance.spawnMush(numOfMushToSpawn);
                mushSpawned = true;
            }
            raining = false;
            cloudy = false;
        }
        else if (DayCount == 3 && timeofDay > 6 && timeofDay < 18)
        {
            cloudy = true;
            raining = false;
            if(timeofDay>=10 && !heli)
            {
                helicopater.SetActive(true);
                HelicopterMove.instance.play = true;
                heli = true;
            }
        }
        else
        {
            //raining = true;
            cloudy = false;
        }

        float xRotation = (timeofDay + 18f) % 24 / 24 * 360f;
        transform.rotation = Quaternion.Euler(xRotation , 0f, 0f);
        MoonLight.transform.rotation = Quaternion.Euler((xRotation + 180f) % 360f, 0f, 0f);
        fadeLight();
        changeTemp();
        if (raining)
        {
            if(QualitySettings.GetQualityLevel() <=1)
            {
                Rain();
                rainEFX.SetActive(true);
            }
            dayLightIntensity = 0.2f;
            UiController.instance.cloudImage.SetActive(true);
        }
        else if (cloudy)
        {
            dayLightIntensity = 0.3f;
            RenderSettings.skybox.SetColor("_SkyTint", cloudyColor);
            RenderSettings.skybox.SetFloat("_SkySize", 0f);
            raining = false;
            RenderSettings.fog = true;
            RenderSettings.fogColor = dayFog;
            RenderSettings.fogEndDistance = 150;
            UiController.instance.cloudImage.SetActive(true);
        }
        else
        {
            if (timeofDay > 6 && timeofDay < 18) {
                Sunny(); }
            rainEFX.SetActive(false);
            dayLightIntensity = 1f;
            UiController.instance.cloudImage.SetActive(false);
        }
    }
    public void matchWeathmakerDayCycle()
    {
        DayCount = WeatherMakerDayNightCycleManagerScript.Instance.Day;
        timeofDay = WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDayTimespan.Hours + 
            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDayTimespan.Minutes/60f;
       
    }
    public void nightTimeSwitch(bool night)
    {
        nightTime = night;
    }
    private void fadeLight()
    {
        if (transform.localEulerAngles.x > 180f && theSun.intensity > 0f)
        {
            theSun.intensity = Mathf.Lerp(theSun.intensity, 0f, Time.deltaTime / fadeTime);
            //timeElapsed += Time.deltaTime;
            if (theSun.intensity <= 0.05f)
            {
                theSun.intensity = 0f;
            }
        }
        else if (transform.localEulerAngles.x <= 180f && theSun.intensity < dayLightIntensity)
        {
            theSun.intensity = Mathf.Lerp(theSun.intensity, dayLightIntensity, Time.deltaTime / fadeTime);
            //timeElapsed += Time.deltaTime;
            if (theSun.intensity >= dayLightIntensity-0.05f)
            {
                theSun.intensity = dayLightIntensity;
            }
        }
    }

    IEnumerator fading(int upOrDown)
    {
        if (upOrDown == 0)
        {
            theSun.intensity = Mathf.Lerp(theSun.intensity, 0f, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            if (theSun.intensity <= 0.05f)
            {
                theSun.intensity = 0f;
            }
        }
        else
        {
            theSun.intensity = Mathf.Lerp(theSun.intensity, dayLightIntensity, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            if (theSun.intensity >= dayLightIntensity-0.05f)
            {
                theSun.intensity = dayLightIntensity;
            }
        }
        yield return new WaitForSeconds(fadeTime);
        timeElapsed = 0;
    }
    private void changeTemp()
    {
        if(transform.localEulerAngles.x > 180f)
        {
            PlayerHealth.instance.StartCold();
            //theSun.cullingMask = night;
        }
        else
        {
            PlayerHealth.instance.EndCold();
            //theSun.cullingMask = day;
        }
    }

    private void Rain()
    {
        RenderSettings.skybox.SetColor("_SkyTint", rainColor);
        RenderSettings.skybox.SetFloat("_SkySize", 0f);
    }
    private void Sunny()
    {
        RenderSettings.skybox.SetColor("_SkyTint", normalColor);
        RenderSettings.skybox.SetFloat("_SkySize", 0.12f);
        RenderSettings.fog = false;
    }

}
