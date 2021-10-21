using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float timeofDay;
    [SerializeField] LayerMask night;
    [SerializeField] LayerMask day;
    [SerializeField] Light theSun;
    [SerializeField] Light MoonLight;
    [SerializeField] float fadeTime = 2f;
    [SerializeField] float moonIntensity = 0.02f;
    [SerializeField] Color rainColor;
    [SerializeField] Color normalColor;
    public float dayLightIntensity = 1f;
    public bool raining;
    [SerializeField] GameObject rainEFX;
    float timeElapsed;
    //how many seconds is a day in game
    private float timeRatio = 6 * 60 / 24;
    public static DayControl instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeofDay += Time.deltaTime / timeRatio;
        timeofDay %= 24;
        float xRotation = ((timeofDay + 18f) % 24) / 24 * 360f;
        transform.rotation = Quaternion.Euler(xRotation , 0f, 0f);
        MoonLight.transform.rotation = Quaternion.Euler((xRotation + 180f) % 360f, 0f, 0f);
        fadeLight();
        changeTemp();
        if (raining)
        {
            Rain();
            rainEFX.SetActive(true);
            dayLightIntensity = 0.2f;
        }
        else
        {
            RainStoped();
            rainEFX.SetActive(false);
            dayLightIntensity = 1f;
        }
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
    }
    private void RainStoped()
    {
        RenderSettings.skybox.SetColor("_SkyTint", normalColor);
    }

}
