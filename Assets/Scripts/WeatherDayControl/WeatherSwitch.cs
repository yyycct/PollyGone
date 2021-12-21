using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class WeatherSwitch : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile Sunny;
    public VolumeProfile Rainny;
    public VolumeProfile Cloudy;
    public VolumeProfile Night;

    private ColorAdjustments ca;
    private float timeElapsed = 0f;
    Color whiteColor = new Color(1f, 1f, 1f);
    Color blueFilterColor = new Color(0.52f, 0.65f, 0.96f);
    private bool inTransition = false;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out ca);
    }

    // Update is called once per frame
    void Update()
    {
        if (DayControl.instance.raining)
        {
            volume.profile = Rainny;
        }
        else if (DayControl.instance.cloudy)
        {
            volume.profile = Cloudy;
        }

        if (DayControl.instance.nightTime && !inTransition)
        {
            ca.colorFilter.value = blueFilterColor;
        }
        else if (!DayControl.instance.nightTime && !inTransition)
        {
            ca.colorFilter.value = whiteColor;
        }
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

    public void DayToNightFilterChange()
    {
        StartCoroutine(ChangeFilter(3f, true));
    }

    public void NightToDayFilterChange()
    {
        StartCoroutine(ChangeFilter(3f, false));
    }
}
