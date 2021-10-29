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

    [SerializeField] private GameObject coldArrow;
    [SerializeField] private GameObject hungerArrow;
    [SerializeField] private GameObject hydrateArrow;
    bool cursorVis = false;
    CursorLockMode lockMode = CursorLockMode.Locked;
    public bool inTutorial = false;

    private void Start()
    {
        _input = playerCollider.instance.GetComponent<StarterAssetsInputs>();
        CloseTutorial();
    }
    private void Update()
    {
        if (_input.anykey && tutorialBubble.activeInHierarchy && inTutorial)
        {
            HideBubble();
            _input.anykey = false;
        }
    }
    
    void CloseTutorial()
    {
        tutorialBubble.gameObject.SetActive(false);
        coldArrow.SetActive(false);
        hungerArrow.SetActive(false);
        hydrateArrow.SetActive(false);
    }

    void ShowBubble(string instruction)
    {
        playerCollider.instance.bagOn = true;
        tutorialText.text = instruction;
        tutorialBubble.gameObject.SetActive(true);
        inTutorial = true;
        continueText.gameObject.SetActive(true);
        UiController.instance.ZeroTimeScale();
        StarterAssetsInputs.instance.SwitchMap();
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
        UiController.instance.OneTimeScale();
        CloseTutorial();
        inTutorial = false;
        StarterAssetsInputs.instance.SwitchMap();
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

    public void ColdTuto()
    {
        if (!PlayerPrefs.HasKey("ColdTutorial"))
        {
            ShowBubble("The cold value is less than 20%, my health is dropping");
            PlayerPrefs.SetInt("ColdTutorial", 1);
            coldArrow.SetActive(true);
        }
    }

    public void HungerTuto()
    {
        if (!PlayerPrefs.HasKey("HungerTutorial"))
        {
            ShowBubble("The hunger value is less than 10%, my health is dropping");
            PlayerPrefs.SetInt("HungerTutorial", 1);
            hungerArrow.SetActive(true);
        }
    }

    public void HydrateTuto()
    {
        if (!PlayerPrefs.HasKey("HydrateTutorial"))
        {
            ShowBubble("The hydrate value is less than 10%, my health is dropping");
            PlayerPrefs.SetInt("HydrateTutorial", 1);
            hydrateArrow.SetActive(true);
        }
    }

    public void FullRaiseHealthTuto()
    {
        if (!PlayerPrefs.HasKey("FullRaiseHealthTutorial"))
        {
            ShowBubble("I am more than 70% full, my health is raising!");
            PlayerPrefs.SetInt("FullRaiseHealthTutorial", 1);
            hydrateArrow.SetActive(true);
        }
    }

    public void BoatEnding()
    {
        StartCoroutine(BoatEndingTimer());
    }

    IEnumerator BoatEndingTimer()
    {
        yield return new WaitForSeconds(2f);
        ShowBubble("You successfully made a boat and escaped this island with it.");
    }
}
