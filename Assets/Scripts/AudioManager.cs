using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    public AudioSource homePageMusic;
    public AudioSource dayOneMusic;
    public AudioSource dayTwoMusic;
    public AudioSource dayThreeMusic;
    public AudioSource nightMusic;
    public AudioSource rainingDayMusic;
    public AudioSource sunnyDayMusic;
    public AudioSource creditMusic;

    public AudioSource oceanWaveSFX;
    public AudioSource radioVoiceSFX;
    public AudioSource radioNoiseeSFX;

    public IEnumerator stopRadio()
    {
        yield return new WaitForSeconds(20f);
        //radioNoiseeSFX.Stop();
        radioVoiceSFX.Stop();
    }
}
