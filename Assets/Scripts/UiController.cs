using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    public Texture WoodSprite;
    public Texture RockSprite;
    public Texture MushSprite;
    public Texture Campfire;
    public Texture AxeSprite;
    public Texture PotSprite;
    public Texture CupSprite;
    public Texture CanSprite;
    public Texture GameConSprite;
    public Texture BatSprite;
    public Texture VaseSprite;
    public Texture EmptySprite;
    public Texture KnifeSprite;
    public Texture CandleSprite;
    public Texture PicFrameSprite;
    public Texture BookSprite;
    //public List<Text> inventoryAmount = new List<Text>();
    public GameObject itemsInBag;
    public GameObject craftButton;
    public GameObject GameOverPanel;
    public GameObject pausePanel;
    public Text deathText;
    public float combineWindowShowTime = 1f;
    public bool cookMode = false;
    public List<RawImage> ItemImage = new List<RawImage>();
    public Dictionary<items.ItemType, GameObject> itemDic = new Dictionary<items.ItemType, GameObject>();
    public GameObject inventoryPanel;
    public GameObject itemPrefab;
    public Animator anim;

    public GameObject craftItems;
    public List<items> itemsInCraft = new List<items>();

    public int itemSelected = -1;
    public int craftItemSelected = -1;

    public GameObject resultPanel;
    public RawImage resultImage;
    public AudioSource craftSuccessSFX;
    public GameObject failPanel;

    public GameObject craftQTEPanel;
    public GameObject StartPanel;
    public GameObject CookPanel;
    public GameObject cookItems;
    public List<items> cookList = new List<items>();

    public GameObject eatArea;
    public GameObject dropArea;

    public Canvas canvas;
    public Transform topLayerTransform;

    public AudioSource Radio;

    public int DragItemNumber = -1;

    private void Awake()
    {
        instance = this;
        StartPanel.SetActive(true);
    }
    public TMP_Text insText;
    void Start()
    {
        Time.timeScale = 0;
        for (int i = 0; i < itemsInBag.transform.childCount; i++)
        {
            ItemImage.Add(itemsInBag.transform.GetChild(i).GetChild(0).GetComponent<RawImage>());
            itemsInBag.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        closeMenu();
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
    }

    public IEnumerator stopRadio()
    {
        yield return new WaitForSeconds(20f);
        Radio.Stop();
    }

    public void changeInsText(string t)
    {
        insText.text = t;
    }

    public void printInventory(items.ItemType _type, int _index, int _amount)
    {
        ItemImage[_index].texture = items.Get2dTexture(_type);
        if (_amount <= 0)
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
        //HideCraftWindow();
    }

    /*public void DropButtonClicked()
    {
        playerCollider.instance.DropItem(true);
    }*/

    /*public void ShowCraftWindow()
    {
        if (!craftMode)
        {
            anim.Play("ShowCraft");
            craftMode = true;
            eatButton.SetActive(false);
            dropButton.SetActive(false);
        }
    }*/

    /*public void HideCraftWindow()
    {
        if (craftMode)
        {
            for (int i = itemsInCraft.Count - 1; i >= 0; i--)
            {
                playerCollider.instance.playerBag.AddItem(itemsInCraft[i]);
            }
            eatButton.SetActive(false);
            dropButton.SetActive(false);
            itemsInCraft.Clear();
            PrintCrafts();
            playerCollider.instance.loopInventory();
            anim.Play("HideCraft");
            craftMode = false;
        }
    }*/

    /*public void Selectable(bool selectable, int index)
    {
        itemsInBag.transform.GetChild(index).GetComponent<Button>().enabled = selectable;
    }*/

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
        if (bag)
        {
            if (playerCollider.instance.playerBag.GetItem(DragItemNumber).itemType == items.ItemType.PurpleMush)
            {
                ThirdPersonController.instance.MoveSpeed -= 0.5f;
                ThirdPersonController.instance.SprintSpeed -= 1f;
                StartCoroutine(GainSpeedBack());
            }
            else if (playerCollider.instance.playerBag.GetItem(DragItemNumber).itemType == items.ItemType.RedMush)
            {
                GameOver("posioned");
            }
        }
        else
        {
            if (itemsInCraft[craftItemSelected].itemType == items.ItemType.PurpleMush)
            {
                ThirdPersonController.instance.MoveSpeed -= 0.5f;
                ThirdPersonController.instance.SprintSpeed -= 1f;
                StartCoroutine(GainSpeedBack());
            }
            else if (itemsInCraft[craftItemSelected].itemType == items.ItemType.RedMush)
            {
                GameOver("posioned");
            }
        }
        Debug.Log("Still Eating");

        PlayerHealth.instance.EatFood(10, bag);
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
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        deathText.text = reason;
        StartCoroutine(Restart());
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
        foreach (Recipe recipe in CraftRecipes.instance.craftRecipes)
        {
            if (recipe.craftMaterials.Count > itemsInCraft.Count) break;
            
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
            playerCollider.instance.playerBag.AddItem(PresetItems.instance.GetItemFromType(i));
        }
        PrintCrafts();

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

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseClicked()
    {
        Time.timeScale = 0;
        closeMenu();
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeClicked()
    {
        playerCollider.instance.bagOn = false;
        Time.timeScale = 1;
        closeMenu();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartButtonClicked()
    {
        StartPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

}

