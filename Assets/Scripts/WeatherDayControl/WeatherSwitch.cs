using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class WeatherSwitch : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile Sunny;
    public VolumeProfile Rainny;
    public VolumeProfile Cloudy;
    public VolumeProfile Night;
    // Start is called before the first frame update
    void Start()
    {
        
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
        else if (DayControl.instance.nightTime)
        {
            volume.profile = Night;
        }
        else
        {
            volume.profile = Sunny;
        }
    }
}
