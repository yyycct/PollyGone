using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;

public class playerCollider : MonoBehaviour
{
    public static playerCollider instance;
    public inventory playerBag = new inventory();
    public items rock, mushroom, wood;
    public bool bagOn = true;
    private void Awake()
    {
        instance = this;
    }
    string inteText;
    public bool interactable = false;
    public int inteCode = -1;
    GameObject targetObject;
    private StarterAssetsInputs _input;
    private int cont = 0;
    public Transform dropPoint;
    bool inCave = false;
    public bool equipped = false;
    public GameObject axeInHand;
    public GameObject rockInHand;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        /*rock = new items(items.ItemType.Rock,  false, true, 0);
        mushroom = new items(items.ItemType.Mushroom, true, false, 0);
        wood = new items(items.ItemType.Wood, false, true, 0);*/
    }
    private void Update()
    {
        if(_input.interact && interactable)
        {
            switch (inteCode)
            {
                case 0:
                    openBox(targetObject);
                    break;
                case 1:
                    pickupItem(targetObject);
                    break;
                case 2:
                    setFire(targetObject);
                    break;
                case 3:
                    cook();
                    break;
                default:
                    break;
            }   
        }
        if (_input.bag)
        {
            if (!UiController.instance.CookPanel.activeInHierarchy)
            {
                if (UiController.instance.inventoryPanel.activeInHierarchy)
                {
                    UiController.instance.closeBag();
                }
                else
                {
                    bagOn = true;
                    UiController.instance.inventoryPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    loopInventory();
                }
            }
            
            _input.bag = false;
        }
        if (_input.restart)
        {
            UiController.instance.Restart();
        }
        if (_input.pause)
        {
            if (UiController.instance.inventoryPanel.activeInHierarchy)
            {
                UiController.instance.closeBag();
            }
            else
            {
                UiController.instance.PauseClicked();
            }
            _input.pause = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _input.interact = false;
        if (other.tag == "box")
        {
            inteText = "(F) Open Box";
            UiController.instance.changeInsText(inteText);
            inteCode = 0;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "tree")
        {
            inteText = "(F) Cut Tree";
            UiController.instance.changeInsText(inteText);
            inteCode = 3;
            targetObject = other.gameObject;
        }
        else if (other.tag == "fire")
        {
            if (!DayControl.instance.raining)
            {
                inteText = "(F) Light Fire";
                UiController.instance.changeInsText(inteText);
                inteCode = 2;
                targetObject = other.gameObject;
            }
            else
            {
                inteText = "It's Raining, can't set fire now";
            }
        }
        else if (other.tag == "rock")
        {
            inteText = "(F) Pick Up Rock";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "branch")
        {
            inteText = "(F) Pick up branch";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "mushroom")
        {
            inteText = "(F) Pick up mushroom";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "purpleMush")
        {
            inteText = "(F) Pick up mushroom";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if(other.tag == "redMush")
        {
            inteText = "(F) Pick up mushroom";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "cup")
        {
            inteText = "(F) Pick up cup";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "pot")
        {
            inteText = "(F) Pick up pot";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "picFrame")
        {
            inteText = "(F) Pick up picture frame";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "book")
        {
            inteText = "(F) Pick up book";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "candle")
        {
            inteText = "(F) Pick up candle";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "gamCon")
        {
            inteText = "(F) Pick up game controller";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "knife")
        {
            inteText = "(F) Pick up knife";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "vase")
        {
            inteText = "(F) Pick up vase";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "can")
        {
            inteText = "(F) Pick up can";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "bat")
        {
            inteText = "(F) Pick up badminton racket";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "axe")
        {
            inteText = "(F) Pick up axe";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "woodBlock")
        {
            inteText = "(F) Pick up wood block";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "radio")
        {
            StartCoroutine(UiController.instance.stopRadio());
        }
        else if (other.tag == "cookedMush")
        {
            inteText = "(F) Pick up mushroom";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }

        else if(other.tag == "coconut")
        {
            inteText = "(F) Pick up coconut";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }
        else if (other.tag == "woodBlock")
        {
            inteText = "(F) Pick up wood block";
            UiController.instance.changeInsText(inteText);
            inteCode = 1;
            targetObject = other.gameObject;
            interactable = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "cave")
        {
            Debug.Log("Enter cave");
            inCave = true;
            PlayerHealth.instance.nearHeat = true;
        }
        else if (other.tag == "campFire")
        {
            if (!other.GetComponent<FireAnimation>().onFire)
            {
                inteText = "(F) Set Fire";
                inteCode = 2;
            }
            else
            {
                inteText = "(F) Cook";
                PlayerHealth.instance.nearHeat = true;
                inteCode = 3;
                PlayerHealth.instance.enterFireRange = true;
            }
            UiController.instance.changeInsText(inteText);
            interactable = true;
            targetObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        wrapUp();
    }

    public void setFire(GameObject targetObject)
    {
        FireAnimation.instance.StartFireAnim();
        wrapUp();
    }

    public void loopInventory()
    {
        for(int i = 0; i < 12; i++)
        {
            UiController.instance.itemsInBag.transform.GetChild(i).GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
            if (i < playerBag.GetSize())
            {
                UiController.instance.printInventory(playerBag.GetItem(i).itemType, i, playerBag.GetItem(i).amount);
                //UiController.instance.Selectable(true, i);
            }
            else
            {
                UiController.instance.printInventory(items.ItemType.Empty, i, 0);
                //UiController.instance.Selectable(false, i);
            }
        }

        UiController.instance.equipArea.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);

        if (equipped)
        {
            UiController.instance.equipArea.SetActive(true);
        }
        else
        {
            UiController.instance.equipArea.SetActive(false);
        }
    }
    public void wrapUp()
    {
        _input.interact = false;
        interactable = false;
        UiController.instance.changeInsText("");
        PlayerHealth.instance.nearHeat = false;
    }
    public void pickupItem(GameObject target)
    {
        switch (target.tag)
        {
            case "rock":
                playerBag.AddItem(PresetItems.instance.rock);
                break;
            case "branch":
                playerBag.AddItem(PresetItems.instance.wood);
                break;
            case "mushroom":
                playerBag.AddItem(PresetItems.instance.mushroom);
                break;
            case "purpleMush":
                playerBag.AddItem(PresetItems.instance.purpleMush);
                break;
            case "redMush":
                playerBag.AddItem(PresetItems.instance.redMush);
                break;
            case "cup":
                checkWater(target);
                playerBag.AddItem(PresetItems.instance.cup);
                break;
            case "can":
                playerBag.AddItem(PresetItems.instance.can);
                break;
            case "picFrame":
                playerBag.AddItem(PresetItems.instance.pic);
                break;
            case "candle":
                playerBag.AddItem(PresetItems.instance.candle);
                break;
            case "pot":
                checkWater(target);
                playerBag.AddItem(PresetItems.instance.pot);
                break;
            case "gamCon":
                playerBag.AddItem(PresetItems.instance.gamCon);
                break;
            case "bat":
                playerBag.AddItem(PresetItems.instance.bat);
                break;
            case "vase":
                checkWater(target);
                playerBag.AddItem(PresetItems.instance.vase);
                break;
            case "book":
                playerBag.AddItem(PresetItems.instance.book);
                break;
            case "knife":
                playerBag.AddItem(PresetItems.instance.knife);
                break;
            case "cookedMush":
                playerBag.AddItem(PresetItems.instance.cookedMush);
                break;
            case "coconut":
                playerBag.AddItem(PresetItems.instance.coconut);
                break;
            case "woodBlock":
                playerBag.AddItem(PresetItems.instance.woodBlock);
                break;
            case "axe":
                playerBag.AddItem(PresetItems.instance.axe);
                break;
        }
        if (!playerBag.bagFull)
        {
            loopInventory();
            wrapUp();
            Destroy(target);
        }
    }
    public void checkWater(GameObject target)
    {
        int waterCount = 0;
        GameObject water = target.transform.GetChild(0).gameObject;
        if (water.transform.localScale.y > 0)
        {
            float waterAmount = water.transform.localScale.x * water.transform.localScale.y * water.transform.localScale.z;
            waterCount = Mathf.CeilToInt(waterAmount / 0.00075f);
        }
        if (waterCount > 0)
        {
            for(int i = 0; i < waterCount; i++)
            {
                playerBag.AddItem(PresetItems.instance.water);
            }
        }
    }
    public void DropItem(bool spawn, bool bag)
    {
        if (bag)
        {
            int selection = UiController.instance.DragItemNumber;
            if (selection >= 0 && selection < playerBag.AllItem.Count())
            {
                if (spawn)
                {
                    GameObject newItem = Instantiate(playerBag.AllItem[selection].Get3dGameObject()
                        , dropPoint.transform.position, dropPoint.transform.rotation);
                }

                playerBag.AllItem[selection].amount--;
                if (playerBag.AllItem[selection].amount <= 0)
                {
                    playerBag.AllItem.Remove(playerBag.AllItem[selection]);
                }
                loopInventory();
            }
        }
        else
        {
            int selection = UiController.instance.craftItemSelected;
            if (selection >= 0 && selection < UiController.instance.itemsInCraft.Count)
            {
                if (spawn)
                {
                    GameObject newItem = Instantiate(UiController.instance.itemsInCraft[selection].Get3dGameObject()
                        , dropPoint.transform.position, dropPoint.transform.rotation);
                }

                UiController.instance.itemsInCraft.Remove(UiController.instance.itemsInCraft[selection]);
                UiController.instance.PrintCrafts();
            }
        }
    }

    public void cook()
    {
        bagOn = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UiController.instance.cookMode = true;
        UiController.instance.CookPanel.SetActive(true);
        UiController.instance.printCookItems();
        wrapUp();
    }

    public void DropCook()
    {
        int selection = UiController.instance.itemSelected;
        if (selection < UiController.instance.cookList.Count)
        {
            StartCoroutine(UiController.instance.ShowResult(items.Get2dTexture(items.ItemType.CookedMush)));
        }
    }

    public void openBox(GameObject target)
    {
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z);
        GameObject item = Instantiate(BoxManager.instance.getItem(cont), pos, Quaternion.identity);
        cont++;
        wrapUp();
        Destroy(target);
    }
}


