using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    // Start is called before the first frame update
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private GameObject coldFigure;
    [SerializeField] private bool coldStart = false;
    [SerializeField] private bool coldCountDown = false;
    [SerializeField] private bool inCold = false;
    [SerializeField] private Text healthText;
    [SerializeField] private Text warningText;
    [SerializeField] private float hunderHealthDropSpeed = 0.3f;
    [SerializeField] private float coldHealthDropSpeed = 0.2f;
    [SerializeField] private float hunderDropSpeed = 0.1f;

    public float healthPoints = 50f;
    public float hungerPoints = 50f;
    public float coldTime = 10f;
    private float coldTimeCounter = 0f;
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

    }
    private void updateHealth()
    {
        if(healthPoints<= 0) 
        { 
            healthPoints = 0;
            if (inCold && hungerPoints<10f)
            {
                UiController.instance.GameOver("Hungry and cold");
            }
            else if (inCold)
            {
                UiController.instance.GameOver("Too cold");
            }
            else if(hungerPoints < 10f)
            {
                UiController.instance.GameOver("I need food");
            }
        }
        else if(healthPoints >= 100) { healthPoints = 100; }
        healthSlider.value = healthPoints;
        healthText.text = ((int)healthPoints).ToString();
    }
    private void updateHunger()
    {
        hungerSlider.value = hungerPoints;
    }
    private void updateCold()
    {
        if (coldStart)
        {
            warningText.text = "It feels a little cold, where can I find some heat?";
        }
        else if (inCold)
        {
            coldFigure.SetActive(true);
            warningText.text = "It feels a little cold, where can I find some heat?";
        }
        else
        {
            coldFigure.SetActive(false);
            warningText.text = "";
        }
    }
    private void decreaseHunger()
    {
        hungerPoints -= Time.deltaTime * hunderDropSpeed;
        if(hungerPoints < 0)
        {
            hungerPoints = 0;
        }
    }

    private void healthEffect()
    {
        if(hungerPoints < 10f)
        {
            healthPoints -= Time.deltaTime * hunderHealthDropSpeed;
        }
        else if(hungerPoints > 90f)
        {
            healthPoints += Time.deltaTime * hunderHealthDropSpeed;
        }
        if (coldStart) 
        {
            coldTimeCounter = coldTime;
            coldCountDown = true;
            coldStart = false;
        }
        if (coldCountDown)
        {
            coldTimeCounter -= Time.deltaTime;
            if(coldTimeCounter <= 0f)
            {
                coldTimeCounter = 0f;
                coldCountDown = false;
                inCold = true;
            }
        }
        if (inCold)
        {
            healthPoints -= Time.deltaTime * coldHealthDropSpeed;
        }
    }
    public void EatFood(int points)
    {
        hungerPoints += points;
        if (hungerPoints >= 100) hungerPoints = 100;
    }
    public void StartCold()
    {
        if(!coldCountDown && !inCold)
        {
            coldStart = true;
        }

    }

    public void EndCold()
    {
        coldStart = false;
        coldCountDown = false;
        inCold = false;
    }
}
