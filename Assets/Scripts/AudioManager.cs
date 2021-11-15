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

    [SerializeField] private AudioSource audioSource;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    public AudioClip rainMusic;
    public AudioClip stormMusic;
    public AudioClip sunriseMusic;
    
    public AudioSource radioVoiceSFX;

    private void Start()
    {
        audioSource.clip = dayMusic;
        audioSource.Play();
    }
      
    public IEnumerator stopRadio()
    {
        yield return new WaitForSeconds(20f);
        //radioNoiseeSFX.Stop();
        radioVoiceSFX.Stop();
    }

    public void MusicTransition(AudioClip nextClip, float fadeOutTime, float fadeInTime)
    {
        StartCoroutine(Delay(nextClip, fadeOutTime, fadeInTime));
    }

    private IEnumerator Delay(AudioClip nextClip, float fadeOutTime, float fadeInTime)
    {
        float elapsed = 0f;
        while (elapsed < fadeOutTime)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        elapsed = 0f;
        audioSource.clip = nextClip;
        audioSource.Play();
        while (elapsed < fadeInTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsed / fadeInTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float fadeOutTime)
    {
        float elapsed = 0f;
        while (elapsed < fadeOutTime)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
    }
    private IEnumerator FadeIn(float fadeInTime)
    {
        audioSource.Play();
        float elapsed = 0f;
        while (elapsed < fadeInTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsed / fadeInTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void OnDayBegin()
    {
        MusicTransition(dayMusic, 3f, 3f);
    }

    public void OnNighBegin()
    {
        MusicTransition(nightMusic, 3f, 3f);
    }

    public void OnSunRise()
    {
        MusicTransition(sunriseMusic, 3f, 3f);
    }
}
