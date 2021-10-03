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
    [SerializeField] private GameObject coldFigure;
    [SerializeField] private bool coldStart = false;
    [SerializeField] private bool inCold = false;
    [SerializeField] private Text healthText;
    [SerializeField] private Text warningText;
    [SerializeField] private float hungerHealthDropSpeed = 0.3f;
    [SerializeField] private float coldHealthDropSpeed = 0.2f;
    [SerializeField] private float hungerDropSpeed = 0.1f;

    public float healthPoints = 50f;
    public float hungerPoints = 50f;
    public float coldTime = 10f;
    private float coldTimeCounter = 0f;
    private float coldValue = 0f;
    public bool nearHeat = false;
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
        healthSlider.fillAmount = healthPoints/100f;
        healthText.text = ((int)healthPoints).ToString();
    }
    private void updateHunger()
    {
        hungerSlider.fillAmount = hungerPoints/100f;
    }
    private void updateCold()
    {
        if (coldStart)
        {
            coldValue += Time.deltaTime * 0.1f;
            coldValue = clampValue(coldValue);
            warningText.text = "It feels a little cold, where can I find some heat?";
            if(coldValue >= 50f)
            {
                inCold = true;
                warningText.text = "I am freezing, Oh no!";
            }
            else
            {
                inCold = false;
                warningText.text = "It feels a little cold, where can I find some heat?";
            }
        }
        else if (inCold)
        {
            //coldFigure.SetActive(true);          
        }
        else
        {
            //coldFigure.SetActive(false);
            warningText.text = "";
        }
        coldSlider.fillAmount = coldValue / 100f;
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
        if (inCold)
        {
            healthPoints -= Time.deltaTime * coldHealthDropSpeed;
        }
        healthPoints = clampValue(healthPoints);
    }
    public void EatFood(int points)
    {
        if (playerCollider.instance.playerBag.AllItem[UiController.instance.itemSelected].itemType
            == items.ItemType.CookedMush)
        {
            hungerPoints += points * 2;
            hungerPoints = clampValue(hungerPoints);
        }
        else
        {
            hungerPoints += points;
            hungerPoints = clampValue(hungerPoints);
        }
    }
    public void StartCold()
    {
        if(!inCold)
        {
            coldStart = true;
        }

    }

    public void EndCold()
    {
        coldStart = false;
        //coldCountDown = false;
        inCold = false;
    }
    public float clampValue(float value)
    {
        if (value >= 100) return 100;
        else if (value <= 0) return 0;
        return value;
    }
}
