using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftQTE : MonoBehaviour
{
    public static CraftQTE instance;
    private void Awake()
    {
        instance = this;
    }
    public Image slider;
    public float craftValue = 0f;
    private float maxCraftValue = 10;
    private float dropPerSecond = 1f;

    private void Start()
    {
        slider.fillAmount = 0f;
    }

    private void Update()
    {
        if (craftValue > 0)
        {
            craftValue -= Time.deltaTime * dropPerSecond;
            slider.fillAmount = craftValue/maxCraftValue;
        }
        if (craftValue>=maxCraftValue)
        {
            gameObject.SetActive(false);
            UiController.instance.DoneCrafting();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
