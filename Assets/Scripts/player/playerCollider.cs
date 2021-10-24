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

    public int craftOrCook = 0; //0 for craft, 1 for cook

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
                    Debug.Log("I am calling");
                    setFire(targetObject);
                    break;
                case 3:
                    //cook();
                    break;
                case 4:
                    //pickUpWater(targetObject);
                    break;
                default:
                    break;
            }   
        }
        if(_input.interactTwo && interactable)
        {
            switch (inteCode)
            {
                case 0:                   
                    break;
                case 1:
                    pickUpWater(targetObject);
                    emptyCountainer(targetObject);
                    break;
                case 2:                    
                    break;
                case 3:                   
                    break;
                case 4:
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
        else if (other.tag == "rock")
        {
            inteText = "(F) Pick Up Rock";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "branch")
        {
            inteText = "(F) Pick up branch";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "mushroom")
        {
            inteText = "(F) Pick up mushroom";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "purpleMush")
        {
            inteText = "(F) Pick up mushroom";
            ChangeInstruText(inteText, other, 1);
        }
        else if(other.tag == "redMush")
        {
            inteText = "(F) Pick up mushroom";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "cup")
        {
            if (checkWater(other.gameObject) > 0)
            {
                UiController.instance.changeInsTwoText("(R) Collect Water");
            }
            inteText = "(F) Pick up cup";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "pot")
        {
            if (checkWater(other.gameObject) > 0)
            {
                UiController.instance.changeInsTwoText("(R) Collect Water");
            }
            inteText = "(F) Pick up pot";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "picFrame")
        {
            inteText = "(F) Pick up picture frame";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "book")
        {
            inteText = "(F) Pick up book";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "candle")
        {
            inteText = "(F) Pick up candle";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "gamCon")
        {
            inteText = "(F) Pick up game controller";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "knife")
        {
            inteText = "(F) Pick up knife";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "vase")
        {
            if (checkWater(other.gameObject) > 0)
            {
                UiController.instance.changeInsTwoText("(R) Collect Water");
            }
            inteText = "(F) Pick up vase";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "can")
        {
            inteText = "(F) Pick up can";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "bat")
        {
            inteText = "(F) Pick up badminton racket";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "axe")
        {
            inteText = "(F) Pick up axe";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "woodBlock")
        {
            inteText = "(F) Pick up wood block";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "radio")
        {
            StartCoroutine(UiController.instance.stopRadio());
        }
        else if (other.tag == "cookedMush")
        {
            inteText = "(F) Pick up cooked mushroom";
            ChangeInstruText(inteText, other, 1);
        }
        else if(other.tag == "coconut")
        {
            inteText = "(F) Pick up coconut";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "soup")
        {
            inteText = "(F) Pick up soup";
            ChangeInstruText(inteText, other, 1);
        }
        else if (other.tag == "woodBlock")
        {
            inteText = "(F) Pick up wood block";
            ChangeInstruText(inteText, other, 1);
        }
    }

    void ChangeInstruText(string inteText, Collider other, int intecode)
    {
        Tutorial.instance.PickUpTuto();
        Debug.Log(other.gameObject.name);
        UiController.instance.changeInsText(inteText);
        inteCode = intecode;
        targetObject = other.gameObject;
        interactable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);
        if (other.tag == "cave")
        {
            CollectFeedback.instance.FoundCave();
            Debug.Log("Enter cave");
            inCave = true;
            PlayerHealth.instance.nearHeat = true;
        }
        else if (other.tag == "campFire")
        {
            Debug.Log(other.name);
            if (!other.gameObject.GetComponent<FireAnimation>().onFire)
            {
                inteText = "(F) Set Fire";
                inteCode = 2;
            }
            else
            {
                inteText = "Open bag start cooking"; //cooking state
                PlayerHealth.instance.nearHeat = true;
                craftOrCook = 1;
                UiController.instance.CookingMode();
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
        targetObject.GetComponent<FireAnimation>().StartFireAnim();
        float time = targetObject.GetComponent<FireAnimation>().FireTime;
        Debug.Log(time);
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
        _input.interactTwo = false;
        interactable = false;
        UiController.instance.changeInsText("");
        UiController.instance.changeInsTwoText("");
        PlayerHealth.instance.nearHeat = false;
        craftOrCook = 0;
        UiController.instance.CraftingMode();
    }
    public void pickUpWater(GameObject target)
    {
        int waterCount = checkWater(target);
        if (waterCount > 0)
        {
            for (int i = 0; i < waterCount; i++)
            {
                playerBag.AddItem(PresetItems.instance.water);
            }
            CollectFeedback.instance.AddtoWater(waterCount);
        }
        wrapUp();
    }
    public void emptyCountainer(GameObject target)
    {
        //GameObject water = target.transform.GetChild(0).gameObject;
        //water.transform.localScale = new Vector3(water.transform.localScale.x, 0f, water.transform.localScale.z);
        switch(target.tag){
            case "pot":
                Instantiate(items.Get3dGameObject(items.ItemType.Pot), target.transform.position, target.transform.rotation);
                break;
            case "cup":
                Instantiate(items.Get3dGameObject(items.ItemType.Cup), target.transform.position, target.transform.rotation);
                break;            
            case "vase":
                Instantiate(items.Get3dGameObject(items.ItemType.Vase), target.transform.position, target.transform.rotation);
                break;
        }
        Destroy(target);
        
    }
    public void pickupItem(GameObject target)
    {
        Tutorial.instance.OpenBagTuto();
        switch (target.tag)
        {
            case "rock":
                playerBag.AddItem(PresetItems.instance.rock);
                CollectFeedback.instance.AddToStone(1);
                break;
            case "branch":
                playerBag.AddItem(PresetItems.instance.wood);
                CollectFeedback.instance.AddToWoodBranch(1);
                break;
            case "mushroom":
                playerBag.AddItem(PresetItems.instance.mushroom);
                CollectFeedback.instance.AddtoMushroom(1);
                break;
            case "purpleMush":
                playerBag.AddItem(PresetItems.instance.purpleMush);
                CollectFeedback.instance.AddtoMushroom(1);
                break;
            case "redMush":
                playerBag.AddItem(PresetItems.instance.redMush);
                CollectFeedback.instance.AddtoMushroom(1);
                break;
            case "cup":
                pickUpWater(target);
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
                pickUpWater(target);
                playerBag.AddItem(PresetItems.instance.pot);
                break;
            case "gamCon":
                playerBag.AddItem(PresetItems.instance.gamCon);
                break;
            case "bat":
                playerBag.AddItem(PresetItems.instance.bat);
                break;
            case "vase":
                pickUpWater(target);
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
            case "soup":
                playerBag.AddItem(PresetItems.instance.soup);
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
    public int checkWater(GameObject target)
    {
        int waterCount = 0;
        GameObject water = target.transform.GetChild(0).gameObject;
        if (water.transform.localScale.y > 0)
        {
            float waterAmount = water.transform.localScale.x * water.transform.localScale.y * water.transform.localScale.z;
            waterCount = Mathf.FloorToInt(waterAmount / 0.00075f);
        }
        return waterCount;
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


