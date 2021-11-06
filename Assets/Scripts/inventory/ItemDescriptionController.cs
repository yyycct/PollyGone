using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionController : MonoBehaviour
{
    public static ItemDescriptionController instance;
    float offset = 10f;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Text itemName;
    [SerializeField] private Text hunger;
    [SerializeField] private Text hydrate;
    [SerializeField] private Text fire;
    [SerializeField] private Text description;

    public void ShowDescription(items item, RectTransform trans)
    {
        if (item != null)
        {
            PositionPanel(trans);
            descriptionPanel.SetActive(true);
            itemName.text = item.itemName;
            hunger.text = "+" + item.hungerValuePlus;
            hydrate.text = "+" + item.hydrateValuePlus;
            fire.text = "+" + item.fireTime;
            description.text = item.description;
        }
        
    }

    private void PositionPanel(RectTransform trans)
    {
        float pivX = 0;
        float pivY = 0;
        float posX = 0;
        float posY = 0;
        if (trans.position.y >= Screen.height * 2 / 5)
        {
            pivY = 1;
            posY = trans.position.y + trans.rect.height + offset;
        }
        else
        {
            pivY = 0;
            posY = trans.position.y - trans.rect.height - offset;
        }

        if (trans.position.x >= Screen.width * 2 / 3)
        {
            pivX = 1;
            posX = trans.position.x - trans.rect.width - offset;
        }
        else
        {
            pivX = 0;
            posX = trans.position.x + trans.rect.width + offset;
        }
        descriptionPanel.GetComponent<RectTransform>().pivot = new Vector2(pivX, pivY);
        descriptionPanel.transform.position = new Vector2(posX, posY);
    }

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
    }
}
