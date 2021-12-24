using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class WeatherSwitch : MonoBehaviour
{
    public static WeatherSwitch instance;
    private void Awake()
    {
        instance = this;
    }
    public Volume Sunny;
    public Volume Rainny;
    public Volume Cloudy;
    public Volume Night;

    private ColorAdjustments ca;
    private float timeElapsed = 0f;
    Color whiteColor = new Color(1f, 1f, 1f);
    Color blueFilterColor = new Color(0.52f, 0.65f, 0.96f);
    private bool inTransition = false;

    private Volume currentWeather;

    private void Start()
    {
        currentWeather = Sunny;
        ResetWeather();
    }

    private void ResetWeather()
    {
        Sunny.weight = 0;
        Night.weight = 0;
        Cloudy.weight = 0;
        Rainny.weight = 0;
        currentWeather.weight = 1;
    }

    void Update()
    {
        /*if (DayControl.instance.raining)
        {
            volume.profile = Rainny;
        }
        else if (DayControl.instance.cloudy)
        {
            volume.profile = Cloudy;
        }*/

        /*if (DayControl.instance.nightTime && !inTransition)
        {
            ca.colorFilter.value = blueFilterColor;
        }
        else if (!DayControl.instance.nightTime && !inTransition)
        {
            ca.colorFilter.value = whiteColor;
        }*/
    }

    IEnumerator ChangeFilter(float duration, bool dayToNight)
    {
        inTransition = true;
        while (timeElapsed < duration)
        {
            if (dayToNight)
                ca.colorFilter.value = Color.Lerp(whiteColor, blueFilterColor, timeElapsed/duration);
            else
                ca.colorFilter.value = Color.Lerp(blueFilterColor, whiteColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;
        inTransition = false;
    }

    public void ChangePPSToNight()
    {
        StartCoroutine(ChangePPSWithDelay(5f, Night));
    }
    public void ChangePPSToDay()
    {
        if (DayControl.instance.DayCount != 3)
            StartCoroutine(ChangePPSWithDelay(5f, Sunny));
    }

    public void ChangePPSToCloudy()
    {
        StartCoroutine(ChangePPSWithDelay(10f, Cloudy));
    }

    public void ChangePPSToRainy()
    {
        StartCoroutine(ChangePPSWithDelay(5f, Rainny));
    }

    IEnumerator ChangePPSWithDelay(float duration, Volume toWeather)
    {
        inTransition = true;
        while (timeElapsed < duration && currentWeather != toWeather)
        {
            currentWeather.weight = Mathf.Lerp(1f, 0f, timeElapsed / duration);
            toWeather.weight = Mathf.Lerp(0f, 1f, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentWeather = toWeather;
        ResetWeather();
        timeElapsed = 0f;
        inTransition = false;
    }

    public void DayToNightFilterChange()
    {
        StartCoroutine(ChangeFilter(3f, true));
    }

    public void NightToDayFilterChange()
    {
        StartCoroutine(ChangeFilter(3f, false));
    }
}
