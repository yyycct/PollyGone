using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private ParticleSystem eatEFX;
    [SerializeField] private ParticleSystem drinkEFX;
    public bool enterFireRange = false;

    public float healthPoints = 50f;
    public float hungerPoints = 50f;
    public float coldBackTime = 10f;
    [SerializeField]
    private float coldValue = 0f;
    public bool nearHeat = false;
    public float hydratePoints = 20f;
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
            string reason = "You died";
            /*if (inCold)
            {
                reason += ", Too Cold";
            }*/
            if(hungerPoints < 10f)
            {
                reason += ", Too hungery";
            }
            if(hydratePoints < 10f)
            {
                reason += ", Need Water";
            }
            UiController.instance.GameOver(reason);
        }
        else if(healthPoints >= 100) { healthPoints = 100; }
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
    }
    private void updateCold()
    {
        if (inCold)
        {
            coldValue += Time.deltaTime * 0.5f;
            coldValue = clampValue(coldValue);
            //warningText.text = "It feels a little cold, where can I find some heat?";
            if(coldValue >= 50f && ! nearHeat)
            {
                //inCold = true;
                warningText.text = "I am freezing, Oh no!";
            }
            else if (coldValue < 50f && !nearHeat)
            {
                //inCold = false;
                warningText.text = "It feels a little cold, where can I find some heat?";
            }
        }
        if (nearHeat)
        {
            coldValue -= Time.deltaTime * 10f;
            coldValue = clampValue(coldValue);
            Tutorial.instance.OnlyShowBubble("Feel warm here");
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
            healthPoints -= Time.deltaTime * hungerHealthDropSpeed;
        }
        else if(hungerPoints > 90f)
        {
            healthPoints += Time.deltaTime * hungerHealthDropSpeed;
        }
        /*if (coldValue >= 50f)
        {
            healthPoints -= Time.deltaTime * coldHealthDropSpeed;
        }*/
        if(hydratePoints <= 10f)
        {
            healthPoints -= Time.deltaTime * 0.4f;
        }
        healthPoints = clampValue(healthPoints);
    }
    public void EatFood(int points)
    {
        hungerPoints += points;
        eatEFX.Play();
        hungerPoints = clampValue(hungerPoints);
    }

    public void drink(int points)
    {
        hydratePoints += points;
        drinkEFX.Play();
        hydratePoints = clampValue(hydratePoints);
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
        coldValue -= Time.deltaTime * coldBackTime;
    }
    public float clampValue(float value)
    {
        if (value >= 100) return 100;
        else if (value <= 0) return 0;
        return value;
    }
}
