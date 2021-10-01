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
    public GameObject dropButton, craftButton;
    public GameObject GameOverPanel;
    public GameObject pausePanel;
    public Text deathText;
    public float combineWindowShowTime = 1f;
    public bool craftMode = false;
    public List<RawImage> ItemImage = new List<RawImage>();
    public Dictionary<items.ItemType, GameObject> itemDic = new Dictionary<items.ItemType, GameObject>();
    public GameObject inventoryPanel;
    public GameObject itemPrefab;
    public Animator anim;
    public GameObject eatButton;

    public GameObject craftItems;
    public List<items> itemsInCraft = new List<items>();

    public int itemSelected = -1;
    public int craftItemSelected = -1;

    public GameObject resultPanel;
    public RawImage resultImage;
    public AudioSource craftSuccessSFX;
    public GameObject failPanel;
    private void Awake()
    {
        instance = this;
    }
    public TMP_Text insText;
    void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < itemsInBag.transform.childCount; i++)
        {
            ItemImage.Add(itemsInBag.transform.GetChild(i).GetChild(0).GetComponent<RawImage>());
            itemsInBag.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        closeMenu();
    }

    void closeMenu()
    {
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        resultPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeInsText(string t)
    {
        insText.text = t;
    }

    /*public Texture GetTexture(items.ItemType item)
    {
        switch (item)
        {
            case items.ItemType.Rock: return RockSprite; break;
            case items.ItemType.Wood: return WoodSprite; break;
            case items.ItemType.Mushroom: return MushSprite; break;
            case items.ItemType.Knife: return KnifeSprite; break;
            case items.ItemType.Can: return CanSprite; break;
            case items.ItemType.Candle: return CandleSprite; break;
            case items.ItemType.Pic: return PicFrameSprite; break;
            case items.ItemType.Bat:return BatSprite; break;
            case items.ItemType.Book: return BookSprite; break;
            case items.ItemType.Vase: return VaseSprite; break;
            case items.ItemType.GamCon: return GameConSprite; break;
            case items.ItemType.Pot: return PotSprite; break;
            case items.ItemType.Cup: return CupSprite; break;
            case items.ItemType.Empty: return EmptySprite; break;
            default:
                break;
        }
        return EmptySprite;
    }*/

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


    public void DropButtonClicked()
    {
        playerCollider.instance.DropItem(true);
    }

    public void ShowCraftWindow()
    {
        if (!craftMode)
        {
            anim.Play("craftShow");
            craftMode = true;
            
        }
        else
        {
            for (int i = itemsInCraft.Count - 1; i >=0; i--)
            {
                playerCollider.instance.playerBag.AddItem(itemsInCraft[i]);
            }
            itemsInCraft.Clear();
            PrintCrafts();
            playerCollider.instance.loopInventory();
            anim.Play("craftHide");
            craftMode = false;
        }
    }

    public void Selectable(bool selectable, int index)
    {
        itemsInBag.transform.GetChild(index).GetComponent<Button>().enabled = selectable;
    }

    public void checkDropButtonState()
    {
        if (itemSelected == -1)
        {
            dropButton.SetActive(false);
        }
    }

    public void AddItemsInCraft()
    {
        if (itemsInCraft.Count < 5)
        {
            itemsInCraft.Add(playerCollider.instance.playerBag.AllItem[itemSelected]);
            playerCollider.instance.DropItem(false);
            PrintCrafts();
        }
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

    public void EatButtonClicked()
    {
        if(playerCollider.instance.playerBag.AllItem[itemSelected].itemType == items.ItemType.PurpleMush)
        {
            ThirdPersonController.instance.MoveSpeed -= 0.5f;
            ThirdPersonController.instance.SprintSpeed -= 1f;
            StartCoroutine(GainSpeedBack());
        }
        else if (playerCollider.instance.playerBag.AllItem[itemSelected].itemType == items.ItemType.RedMush)
        {
            GameOver("posioned");
        }
        PlayerHealth.instance.EatFood(10);
        playerCollider.instance.DropItem(false);
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
    }


    public void CraftButtonClicked()
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

        yield return new WaitForSeconds(3f);
        resultPanel.SetActive(false);
        
        playerCollider.instance.loopInventory();
    }


    public void Restart()
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
}

