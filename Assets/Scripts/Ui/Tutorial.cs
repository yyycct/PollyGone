using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private StarterAssetsInputs _input;
    public static Tutorial instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject tutorialBubble;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private TMP_Text continueText;
    bool bagOnBefore = false;
    private bool inTutorial = false;

    private void Start()
    {
        _input = playerCollider.instance.GetComponent<StarterAssetsInputs>();
        CloseTutorial();
    }
    private void Update()
    {
        if (_input.jump && tutorialBubble.activeInHierarchy)
        {
            HideBubble();
            _input.jump = false;
        }
    }
    
    void CloseTutorial()
    {
        tutorialBubble.gameObject.SetActive(false);
    }

    void ShowBubble(string instruction)
    {
        bagOnBefore = playerCollider.instance.bagOn;
        playerCollider.instance.bagOn = true;
        UiController.instance.ZeroTimeScale();
        tutorialText.text = instruction;
        tutorialBubble.gameObject.SetActive(true);
        inTutorial = true;
        continueText.gameObject.SetActive(true);
    }

    public void OnlyShowBubble(string instruction)
    {
        if (!inTutorial)
        {
            tutorialText.text = instruction;
            tutorialBubble.gameObject.SetActive(true);
            inTutorial = false;
            continueText.gameObject.SetActive(false);
        }
        
    }

    void HideBubble()
    {
        playerCollider.instance.bagOn = bagOnBefore;
        UiController.instance.OneTimeScale();
        CloseTutorial();
        inTutorial = false;
    }

    public void OnlyHideBubble()
    {
        if (!inTutorial)
        {
            CloseTutorial();
        }
    }

    public void WalkTuto()
    {
        if (!PlayerPrefs.HasKey("WalkTutorial"))
        {
            ShowBubble("I should walk around ('wasd')");
            PlayerPrefs.SetInt("WalkTutorial", 1);
        }
    }

    public void PickUpTuto()
    {
        if (!PlayerPrefs.HasKey("PickUpTutorial"))
        {
            ShowBubble("What is this?");
            PlayerPrefs.SetInt("PickUpTutorial", 1);
        }
    }

    public void OpenBagTuto()
    {
        if (!PlayerPrefs.HasKey("OpenTutorial"))
        {
            ShowBubble("It should be in my pocket. \n(Press TAB to open/close bag)");
            PlayerPrefs.SetInt("OpenTutorial", 1);
        }
    }

    public void DragAndDropTuto()
    {
        if (!PlayerPrefs.HasKey("DragAndDropTutorial"))
        {
            ShowBubble("I think I can drag thing around");
            PlayerPrefs.SetInt("DragAndDropTutorial", 1);
        }
    }

    public void CraftTuto()
    {
        if (!PlayerPrefs.HasKey("CraftTuto"))
        {
            ShowBubble("Can I make something new?");
            PlayerPrefs.SetInt("CraftTuto", 1);
        }
    }

    public void CookTuto()
    {
        if (!PlayerPrefs.HasKey("CookTuto"))
        {
            ShowBubble("I should now be able to cook something around fire.");
            PlayerPrefs.SetInt("CookTuto", 1);
        }
    }

    public void AddWoodTuto()
    {
        if (!PlayerPrefs.HasKey("AddWoodTutorial"))
        {
            ShowBubble("Drop something to the fire to extend fire duration.");
            PlayerPrefs.SetInt("AddWoodTutorial", 1);
        }
    }
}
