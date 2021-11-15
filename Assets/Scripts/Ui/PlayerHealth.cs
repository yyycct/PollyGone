using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    // Start is called before the first frame update
    [SerializeField] private Image healthSlider;
    [SerializeField] private Image hungerSlider;
    [SerializeField] private Image coldSlider;
    [SerializeField] private Image hydrateSlider;
    [SerializeField] private GameObject coldFigure;
    [SerializeField] private bool coldStart = false;
    [SerializeField] private bool inCold = false;
    [SerializeField] private Text healthText;
    [SerializeField] private Text warningText;
    [SerializeField] private float hungerHealthDropSpeed = 0.3f;
    [SerializeField] private float coldHealthDropSpeed = 0.2f;
    [SerializeField] private float hungerDropSpeed = 0.1f;
    [SerializeField] private float hydrateDropSpeed = 0.2f;
    [SerializeField] private float coldDropSpeed = 0.2f;
    [SerializeField] private ParticleSystem eatEFX;
    [SerializeField] private ParticleSystem drinkEFX;
    [SerializeField] private Color Green;
    [SerializeField] private Color Red;
    public bool enterFireRange = false;
    public float healthPoints = 10f;
    public float hungerPoints = 50f;
    public float coldBackTime = 50f;
    public float coldValue = 70f;
    public bool nearHeat = false;
    public float hydratePoints = 20f;
    private float vignetteValue = 0f;
    private Volume volume;
    public VolumeProfile Sunny;
    public VolumeProfile Rainny;
    public VolumeProfile Cloudy;
    private Vignette vignette;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCold();
        decreaseHunger();
        healthEffect();
        updateHealth();
        updateHunger();
        updateHydrate();

    }
    private void updateHealth()
    {
        if(healthPoints<= 0) 
        { 
            healthPoints = 0;
            string reason = "";
            if(hungerPoints < 10f)
            {
                reason += ", Too hungery";
            }
            if(hydratePoints < 10f)
            {
                reason += ", Thirsty";
            }
            UiController.instance.GameOver(reason);
        }
        else if (healthPoints <= 5f)
        {
            if (Sunny.TryGet<Vignette>(out var vignette))
            {
                vignetteValue += 0.05f * Time.deltaTime;
                if (vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette.intensity.Override(vignetteValue);
            }
            if (Rainny.TryGet<Vignette>(out var vignette2))
            {
                vignetteValue += 0.05f * Time.deltaTime;
                if (vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette2.intensity.Override(vignetteValue);
            }
            if (Cloudy.TryGet<Vignette>(out var vignette3))
            {
                vignetteValue += 0.05f * Time.deltaTime;
                if (vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette3.intensity.Override(vignetteValue);
            }
        }
        else if (healthPoints > 5f)
        {
            if (Sunny.TryGet<Vignette>(out var vignette))
            {
                vignetteValue -= 0.01f;
                if(vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette.intensity.Override(vignetteValue);
            }
            if (Rainny.TryGet<Vignette>(out var vignette2))
            {
                vignetteValue -= 0.01f;
                if (vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette2.intensity.Override(vignetteValue);
            }
            if (Cloudy.TryGet<Vignette>(out var vignette3))
            {
                vignetteValue -= 0.01f;
                if (vignetteValue < 0) { vignetteValue = 0f; }
                else if (vignetteValue > 1) { vignetteValue = 1f; }
                vignette3.intensity.Override(vignetteValue);
            }
        }
        if(healthPoints >= 100) { healthPoints = 100; }
        healthSlider.fillAmount = healthPoints/100f;
        healthText.text = ((int)healthPoints).ToString();
    }
    private void updateHydrate()
    {
        hydratePoints -= Time.deltaTime * hydrateDropSpeed;
        hydrateSlider.fillAmount = hydratePoints / 100f;
    }
    private void updateHunger()
    {
        hungerSlider.fillAmount = hungerPoints/100f;
        if(hungerPoints < 10f)
        {
            hungerSlider.color = Red;
        }
        else{
            hungerSlider.color = Green;
        }
    }
    private void updateCold()
    {
        if (inCold)
        {
            coldValue -= Time.deltaTime * coldDropSpeed;
            coldValue = clampValue(coldValue);
            //warningText.text = "It feels a little cold, where can I find some heat?";
            if(coldValue <= 20f && ! nearHeat)
            {
                //inCold = true;
                Tutorial.instance.ColdTuto();
                Tutorial.instance.OnlyShowBubble("I am freezing, Oh no!");
            }
            else if (coldValue > 20f && !nearHeat && coldValue < 40f)
            {
                //inCold = false;
                Tutorial.instance.OnlyShowBubble("It feels a little cold, where can I find some heat?");
            }
        }
        if (nearHeat)
        {
            if (coldValue <= 80)
            {
                coldValue += Time.deltaTime * 10f;
            }
            coldValue = clampValue(coldValue);
            if (!playerCollider.instance.bagOn && !Tutorial.instance.inTutorial)
            {
                Tutorial.instance.OnlyShowBubble("Feel warm here");
            }
        }
        if (!nearHeat && !inCold)
        {
            Tutorial.instance.OnlyHideBubble();
        }
        coldSlider.fillAmount = coldValue / 100f;
    }
    private IEnumerator resetText()
    {
        enterFireRange = false;
        yield return new WaitForSeconds(3f);
        warningText.text = "";
        Tutorial.instance.OnlyHideBubble();
    }
    private void decreaseHunger()
    {
        hungerPoints -= Time.deltaTime * hungerDropSpeed;
        hungerPoints = clampValue(hungerPoints);
    }

    private void healthEffect()
    {
        if(hungerPoints < 10f)
        {
            Tutorial.instance.HungerTuto();
            healthPoints -= Time.deltaTime * hungerHealthDropSpeed;
            hungerSlider.color = Red;
        }
        else
        {
            hungerSlider.color = Green;
        }
        if(hungerPoints > 50f)
        {
            Tutorial.instance.FullRaiseHealthTuto();
            healthPoints += Time.deltaTime * hungerHealthDropSpeed;
        }

        if (coldValue <= 20f)
        {
            Tutorial.instance.ColdTuto();
            healthPoints -= Time.deltaTime * coldHealthDropSpeed;
            coldSlider.color = Red;
        }
        else
        {
            coldSlider.color = Green;
        }
        if(hydratePoints <= 10f)
        {
            Tutorial.instance.HydrateTuto();
            healthPoints -= Time.deltaTime * 0.4f;
            hydrateSlider.color = Red;
        }
        else
        {
            hydrateSlider.color = Green;
        }       
        healthPoints = clampValue(healthPoints);
    }
    public void EatFood(int points)
    {
        if (points > 0)
        {
            hungerPoints += points;
            eatEFX.Play();
            hungerPoints = clampValue(hungerPoints);
        }
    }

    public void drink(int points)
    {
        if (points > 0)
        {
            hydratePoints += points;
            drinkEFX.Play();
            hydratePoints = clampValue(hydratePoints);
        }
    }

    public void StartCold()
    {
        if(!inCold)
        {
            inCold = true;
        }

    }

    public void EndCold()
    {
        inCold = false;
        if (coldValue <= 60)
        {
            coldValue += Time.deltaTime * coldBackTime;
        }
        
    }
    public float clampValue(float value)
    {
        if (value >= 100) return 100;
        else if (value <= 0) return 0;
        return value;
    }
}
