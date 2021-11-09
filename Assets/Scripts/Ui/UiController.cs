using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UiController : MonoBehaviour
{
    [SerializeField] private Animation StartingAnimation;
    public static UiController instance;
    public Texture EmptySprite;
    //public List<Text> inventoryAmount = new List<Text>();
    public GameObject itemsInBag;
    public GameObject craftButton;
    public GameObject GameOverPanel;
    public GameObject pausePanel;
    public Text deathText;
    public float combineWindowShowTime = 1f;
    public bool cookMode = false;
    public Text craftCookInstructionText;
    public List<RawImage> ItemImage = new List<RawImage>();
    public Dictionary<items.ItemType, GameObject> itemDic = new Dictionary<items.ItemType, GameObject>();
    public GameObject inventoryPanel;
    public GameObject itemPrefab;

    public GameObject craftItems;
    public List<items> itemsInCraft = new List<items>();

    public int itemSelected = -1;
    public int craftItemSelected = -1;

    public GameObject resultPanel;
    public RawImage resultImage;
    public AudioSource craftSuccessSFX;
    public GameObject failPanel;

    [SerializeField] private Sprite craftSprite;
    [SerializeField] private Sprite cookSprite;
    public GameObject craftQTEPanel;
    [SerializeField] private Image craftQTEImage;
    public GameObject StartPanel;
    public GameObject CookPanel;
    public GameObject cookItems;
    public List<items> cookList = new List<items>();

    public GameObject eatArea;
    public GameObject dropArea;
    public GameObject equipArea;

    public Text dropAreaText;
    public GameObject dropAreaFireImage;

    public Canvas canvas;
    public Transform topLayerTransform;
    public GameObject uiviewAxe;
    public GameObject uiviewRock;

    public items equipItem = null;
    public AudioSource Radio;
    public int DragItemNumber = -1;
    public bool DraggingTool = false;
    public int lastInBagNumber = -1;
    public TMP_Text insText;
    public TMP_Text insTwoText;

    public GameObject rescuePanel;
    public Image rescueImage;
    public Sprite boatSprite;
    public Sprite helicopterSprite;

    public GameObject cloudImage;
    public GameObject CreditPanel;

    public bool isDragging = false;
    private void Awake()
    {
        instance = this;
        //StartPanel.SetActive(true);
    }

    void Start()
    {
        //ZeroTimeScale();
        for (int i = 0; i < itemsInBag.transform.childCount; i++)
        {
            ItemImage.Add(itemsInBag.transform.GetChild(i).GetChild(0).GetComponent<RawImage>());
            itemsInBag.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        closeMenu();
        CraftingMode();
        StartCoroutine(PlayStartingAnimaiton());
    }

    IEnumerator PlayStartingAnimaiton()
    {
        StartingAnimation.Play();
        yield return new WaitForSeconds(StartingAnimation.clip.length);
        //StartingAnimation.Stop();
    }

    void closeMenu()
    {
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        resultPanel.SetActive(false);
        failPanel.SetActive(false);
        craftQTEPanel.SetActive(false);
        CookPanel.SetActive(false);
        eatArea.SetActive(false);
        dropArea.SetActive(false);
        equipArea.SetActive(false);
        rescuePanel.SetActive(false);
        CreditPanel.SetActive(false);
    }

    public void changeInsText(string t)
    {
        insText.text = t;
    }
    public void changeInsTwoText(string t)
    {
        insTwoText.text = t;
    }
    public void printInventory(items.ItemType _type, int _index, int _amount)
    {
        ItemImage[_index].texture = items.Get2dTexture(_type);
        if (_amount <= 1)
        {
            itemsInBag.transform.GetChild(_index).GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        else
        {
            itemsInBag.transform.GetChild(_index).GetChild(1).GetComponent<TMP_Text>().text = _amount.ToString();
        }
    }

    public void printCookItems()
    {
        cookList.Clear();
        foreach (items item in playerCollider.instance.playerBag.AllItem)
        {
            if (item.ediable && item.itemType != items.ItemType.CookedMush)
            {
                cookList.Add(item);
            }
        }

        for (int i = 0; i < cookItems.transform.childCount; i++)
        {
            if (i < cookList.Count)
            {
                cookItems.transform.GetChild(i).GetChild(0).GetComponent<RawImage>().texture =
                    items.Get2dTexture(cookList[i].itemType);
                cookItems.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text =
                    cookList[i].amount.ToString();
            }
            else
            {
                cookItems.transform.GetChild(i).GetChild(0).GetComponent<RawImage>().texture = EmptySprite;
                cookItems.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
            }
        }
    }

    public void CookingMode()
    {
        Color yellowColor = new Color(0.94f, 0.84f, 0.4f, 1f);
        craftButton.transform.GetChild(0).GetComponent<Text>().text = "Cook";
        craftButton.transform.GetChild(0).GetComponent<Text>().color = yellowColor;
        craftCookInstructionText.text = "Cook something new...";
        craftCookInstructionText.color = yellowColor;
        craftQTEImage.sprite = cookSprite;

        dropAreaFireImage.SetActive(true);
        dropAreaText.text = "Drop To Fire";
    }

    public void CraftingMode()
    {
        Color whiteColor = new Color(1f, 1f, 1f, 1f);
        craftButton.transform.GetChild(0).GetComponent<Text>().text = "Craft";
        craftButton.transform.GetChild(0).GetComponent<Text>().color = whiteColor;
        craftCookInstructionText.text = "Drop Items here to make something new...";
        craftCookInstructionText.color = whiteColor;
        craftQTEImage.sprite = craftSprite;

        dropAreaFireImage.SetActive(false);
        dropAreaText.text = "Drop To Ground";
    }
    public void CookDoneClicked()
    {
        playerCollider.instance.bagOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cookMode = false;
        CookPanel.SetActive(false);
    }

    public void closeBag()
    {
        playerCollider.instance.bagOn = false;
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddItemsInCraft()
    {
        if (itemsInCraft.Count < 5)
        {
            itemsInCraft.Add(playerCollider.instance.playerBag.AllItem[DragItemNumber]);
            playerCollider.instance.DropItem(false, true);
            PrintCrafts();
        }
        playerCollider.instance.loopInventory();
    }

    public void RemoveItemsFromCraft()
    {
        playerCollider.instance.playerBag.AddItem(itemsInCraft[craftItemSelected]);
        playerCollider.instance.loopInventory();
        itemsInCraft.Remove(itemsInCraft[craftItemSelected]);
        PrintCrafts();
    }

    public void RemoveLastAddedItemInBag()
    {
        playerCollider.instance.playerBag.AllItem.RemoveAt(lastInBagNumber);
        playerCollider.instance.loopInventory();
    }


    public void PrintCrafts()
    {
        for (int i = 0; i < 5; i++)
        {
            craftItems.transform.GetChild(i).GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
            if (i < itemsInCraft.Count)
            {
                craftItems.transform.GetChild(i).GetChild(0).GetComponent<RawImage>().texture
                    = items.Get2dTexture(itemsInCraft[i].itemType);
            }
            else
            {
                craftItems.transform.GetChild(i).GetChild(0).GetComponent<RawImage>().texture
                    = EmptySprite;
            }
        }
    }

    public void EatButtonClicked(bool bag)
    {
        Debug.Log("Eating");
        items item;
        items.ItemType objType;
        if (bag) item = playerCollider.instance.playerBag.GetItem(DragItemNumber);
        else item = itemsInCraft[craftItemSelected];

        if (bag) objType = playerCollider.instance.playerBag.GetItem(DragItemNumber).itemType;
        else objType = itemsInCraft[craftItemSelected].itemType;

        PlayerHealth.instance.EatFood(item.hungerValuePlus);
        PlayerHealth.instance.drink(item.hydrateValuePlus);
        if (objType == items.ItemType.PurpleMush)
        {
            ThirdPersonController.instance.MoveSpeed -= 0.5f;
            ThirdPersonController.instance.SprintSpeed -= 1f;
            StartCoroutine(GainSpeedBack());
        }
        else if (objType == items.ItemType.RedMush)
        {
            GameOver("posioned");
        }
        
        playerCollider.instance.DropItem(false, bag);
        
        
    }
    public IEnumerator GainSpeedBack()
    {
        yield return new WaitForSeconds(10f);
        ThirdPersonController.instance.MoveSpeed += 0.5f;
        ThirdPersonController.instance.SprintSpeed += 1f;
    }
    public void GameOver(string reason)
    {
        GameOverPanel.SetActive(true);
        deathText.text = "You died, " + reason;
        StartCoroutine(Restart(reason));
    }

    public void CraftButtonClicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        craftQTEPanel.SetActive(true);
        CraftQTE.instance.craftValue = 0f;
        CraftQTE.instance.slider.fillAmount = 0f;
    }

    public void DoneCrafting()
    {
        bool hasRecipe = false;
        List<Recipe> targetRecipes = new List<Recipe>();
        if (playerCollider.instance.craftOrCook == 0)
        {
            targetRecipes = CraftRecipes.instance.craftRecipes;
        }
        else if (playerCollider.instance.craftOrCook == 1)
        {
            targetRecipes = CraftRecipes.instance.cookingRecipes;
        }

        foreach (Recipe recipe in targetRecipes)
        {
            if (recipe.craftMaterials.Count > itemsInCraft.Count) continue;
            
            List<items.ItemType> remains = new List<items.ItemType>();
            foreach (items _item in itemsInCraft) remains.Add(_item.itemType);
            List<items.ItemType> tempList = new List<items.ItemType>();
            foreach (items.ItemType _itemType in recipe.craftMaterials) tempList.Add(_itemType);

            for (int i = 0; i < itemsInCraft.Count; i++)
            {
                if (tempList.Contains(itemsInCraft[i].itemType))
                {
                    tempList.Remove(itemsInCraft[i].itemType);
                    remains.Remove(itemsInCraft[i].itemType);

                    if (tempList.Count <=0)
                    {
                        hasRecipe = true;
                        StartCoroutine(ShowResult(recipe, remains));
                        return;
                    }
                }
            }
            
        }
        if (!hasRecipe)
        {
            StartCoroutine(FailTextShow());
        }
    }

    public IEnumerator FailTextShow()
    {
        failPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        failPanel.SetActive(false);
    }

    public IEnumerator ShowResult(Recipe recipe, List<items.ItemType> remains)
    {
        resultPanel.SetActive(true);
        craftSuccessSFX.Play();
        List<items.ItemType> result = new List<items.ItemType>();
        resultImage.texture = items.Get2dTexture(recipe.craftResult);
        result.Add(recipe.craftResult);

        itemsInCraft.Clear();
        foreach (items.ItemType _itemType in remains)
        {
            itemsInCraft.Add(PresetItems.instance.GetItemFromType(_itemType));
        }

        foreach (var i in result)
        {
            Debug.Log(PresetItems.instance.GetItemFromType(i).itemType);
            playerCollider.instance.playerBag.AddItem(PresetItems.instance.GetItemFromType(i));
        }
        PrintCrafts();

        if (recipe.craftResult == items.ItemType.Axe)
        {
            CollectFeedback.instance.AxeCrafted();
        }
        else if (recipe.craftResult == items.ItemType.CampFire)
        {
            CollectFeedback.instance.CampfireCrafted();
        }

        yield return new WaitForSeconds(1.5f);
        resultPanel.SetActive(false);
        
        playerCollider.instance.loopInventory();
    }

    public IEnumerator ShowResult(Texture texture)
    {
        resultImage.texture = texture;
        resultPanel.SetActive(true);
        craftSuccessSFX.Play();
        DropCookedItem();
        printCookItems();
        yield return new WaitForSeconds(1.5f);
        resultPanel.SetActive(false);
        //playerCollider.instance.playerBag.AddItem(PresetItems.instance.GetItemFromType(items.ItemType.CookedMush));
        GameObject newCooked = Instantiate(items.Get3dGameObject(items.ItemType.CookedMush));
        newCooked.transform.position = playerCollider.instance.dropPoint.position;
    }

    public void DropCookedItem()
    {
        int selection = itemSelected;
        if (itemSelected >= 0 && itemSelected < cookList.Count)
        {
            items.ItemType it = cookList[selection].itemType;
            for (int i = 0; i < playerCollider.instance.playerBag.GetSize(); i++)
            {
                if (playerCollider.instance.playerBag.AllItem[i].itemType == it)
                {
                    playerCollider.instance.playerBag.AllItem[i].amount--;
                    if (playerCollider.instance.playerBag.AllItem[i].amount <= 0)
                    {
                        playerCollider.instance.playerBag.AllItem.Remove(playerCollider.instance.playerBag.AllItem[i]);
                    }
                }
            }
            playerCollider.instance.loopInventory();
        }
    }

    public IEnumerator Restart(string reason)
    {
        playerCollider.instance.bagOn = true;
        yield return new WaitForSeconds(3f);
        playerCollider.instance.bagOn = false;
        CollectFeedback.instance.AddDeath(1);
        CollectFeedback.instance.endTimer();
        CollectFeedback.instance.DeathReason(reason);
        OneTimeScale();
        SceneManager.LoadScene("Island");
    }

    public void RestartButtonClicked()
    {
        OneTimeScale();
        SceneManager.LoadScene("Island");
    }

    public void PauseClicked()
    {
        ZeroTimeScale();
        closeMenu();
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeClicked()
    {
        playerCollider.instance.bagOn = false;
        OneTimeScale();
        closeMenu();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartButtonClicked()
    {
        //CollectFeedback.instance.WriteMetricsToFile();
        //AudioManager.instance.homePageMusic.Stop();
        AudioManager.instance.oceanWaveSFX.Play();
        //AudioManager.instance.dayOneMusic.Play();
        AudioManager.instance.radioVoiceSFX.Play();
        StartPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OneTimeScale();
        playerCollider.instance.bagOn = false;
        Tutorial.instance.WalkTuto();
        CollectFeedback.instance.startTimer();
    }

    public void Rescued(int reason)
    {
        if (reason == 0)
        {
            rescueImage.sprite = boatSprite;
        }
        else if (reason == 1)
        {
            rescueImage.sprite = helicopterSprite;
        }
        playerCollider.instance.bagOn = true;
        StartCoroutine(CreditTimer());
        rescuePanel.SetActive(true);
    }
    IEnumerator CreditTimer()
    {
        yield return new WaitForSeconds(4f);
        closeMenu();
        ZeroTimeScale();
        CreditPanel.SetActive(true);
    }
    public void ZeroTimeScale()
    {
        Time.timeScale = 0;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }

    public void OneTimeScale()
    {
        Time.timeScale = 1;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    }

    public void QuitButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void SetQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
    }

}

