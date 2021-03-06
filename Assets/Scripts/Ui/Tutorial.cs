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
    [SerializeField] private GameObject DropArrow;
    [SerializeField] private GameObject EatArrow;
    [SerializeField] private GameObject EquipArrow;

    [SerializeField] private GameObject bagFullText;
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
        DropArrow.SetActive(false);
        EatArrow.SetActive(false);
        EquipArrow.SetActive(false);
    }

    public void CloseArrows()
    {
        coldArrow.SetActive(false);
        hungerArrow.SetActive(false);
        hydrateArrow.SetActive(false);
        DropArrow.SetActive(false);
        EatArrow.SetActive(false);
        EquipArrow.SetActive(false);
    }

    void ShowBubble(string instruction)
    {
        //playerCollider.instance.bagOn = true;
        tutorialText.text = instruction;
        tutorialBubble.gameObject.SetActive(true);
        inTutorial = true;
        StartCoroutine(HideBubbleAfter(3f));
        //continueText.gameObject.SetActive(true);
        //StarterAssetsInputs.instance.SwitchMap();
        //UiController.instance.ZeroTimeScale();
    }

    void ShowBubble(string instruction, float duration)
    {
        //playerCollider.instance.bagOn = true;
        tutorialText.text = instruction;
        tutorialBubble.gameObject.SetActive(true);
        inTutorial = true;
        StartCoroutine(HideBubbleAfter(duration));
    }

    IEnumerator HideBubbleAfter(float second)
    {
        yield return new WaitForSeconds(second);
        CloseTutorial();
        inTutorial = false;
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
        playerCollider.instance.bagOn = false;
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
            ShowBubble("WASD to walk, hold SHIFT to run, ESC to pause", 5f);
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
            ShowBubble("I should be able to drag items around", 5f);
            PlayerPrefs.SetInt("DragAndDropTutorial", 1);
        }
    }

    public void UseItemTuto(items item)
    {
        if (!PlayerPrefs.HasKey("PlaceOnGroundTutorial"))
        {
            DropArrow.SetActive(true);
            ShowBubble("You can drop the item to the drop area to place it on the ground", 3f);
            PlayerPrefs.SetInt("PlaceOnGroundTutorial", 1);
        }
        else if (!PlayerPrefs.HasKey("EquipTutorial"))
        {
            if (item.usable)
            {
                EquipArrow.SetActive(true);
                ShowBubble("Drop to the equip area to hold in hand", 3f);
                PlayerPrefs.SetInt("EquipTutorial", 1);
            }
        }
        else if (!PlayerPrefs.HasKey("EatTutorial"))
        {
            if (item.ediable)
            {
                EatArrow.SetActive(true);
                ShowBubble("Drop to the eat area to eat or drink", 3f);
                PlayerPrefs.SetInt("EatTutorial", 1);
            }
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
            ShowBubble("I am more than 50% full, my health is raising!");
            PlayerPrefs.SetInt("FullRaiseHealthTutorial", 1);
            hungerArrow.SetActive(true);
        }
    }

    public void ReadJournalTuto()
    {
        if (!PlayerPrefs.HasKey("ReadJournalTutorial"))
        {
            ShowBubble("Now I can read to journal by pressing J");
            PlayerPrefs.SetInt("ReadJournalTutorial", 1);
        }
    }

    public void AttackTuto()
    {
        if (!PlayerPrefs.HasKey("AttackTutorial"))
        {
            ShowBubble("Left mouse click to hit with your tool");
            PlayerPrefs.SetInt("AttackTutorial", 1);
        }
    }

    public void HelicopterTuto()
    {
        if (!PlayerPrefs.HasKey("HelicopterTutorial"))
        {
            ShowBubble("A helicopter! What should I do to draw their attention?", 10f);
            PlayerPrefs.SetInt("HelicopterTutorial", 1);
        }
    }

    public void BoatEnding()
    {
        StartCoroutine(BoatEndingTimer());
    }

    IEnumerator BoatEndingTimer()
    {
        yield return new WaitForSeconds(3f);
        UiController.instance.Rescued(0);
    }

    public void HelicopterEnding()
    {
        StartCoroutine(HelicopterEndingTimer());
    }

    IEnumerator HelicopterEndingTimer()
    {
        yield return new WaitForSeconds(3f);
        UiController.instance.Rescued(1);
    }

    public void BagIsFull()
    {
        bagFullText.SetActive(true);
        StartCoroutine(BagIsFullDisappear());
    }

    IEnumerator BagIsFullDisappear()
    {
        yield return new WaitForSeconds(3f);
        bagFullText.SetActive(false);
    }
}
