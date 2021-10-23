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
        playerCollider.instance.bagOn = true;
        UiController.instance.ZeroTimeScale();
        tutorialText.text = instruction;
        tutorialBubble.gameObject.SetActive(true);
        inTutorial = true;
    }

    void HideBubble()
    {
        playerCollider.instance.bagOn = false;
        UiController.instance.OneTimeScale();
        CloseTutorial();
        inTutorial = false;
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
}
