using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class playerCollider : MonoBehaviour
{
    public static playerCollider instance;
    public inventory playerBag = new inventory();
    public items rock, mushroom, wood;
    public bool bagOn = false;
    private void Awake()
    {
        instance = this;
    }
    string inteText;
    bool interactable = false;
    int inteCode = -1;
    GameObject targetObject;
    private StarterAssetsInputs _input;
    private int cont = 0;
    public Transform dropPoint;
    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        rock = new items(items.ItemType.Rock,  false, true, 0);
        mushroom = new items(items.ItemType.Mushroom, true, false, 0);
        wood = new items(items.ItemType.Wood, false, true, 0);
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
                default:
                    break;
            }   
        }
        if (_input.bag)
        {
            if (UiController.instance.inventoryPanel.activeInHierarchy)
            {
                UiController.instance.closeBag();
            }
            else
            {
                bagOn = true;
                UiController.instance.dropButton.SetActive(false);
                UiController.instance.eatButton.SetActive(false);
                UiController.instance.inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                loopInventory();
            }
            _input.bag = false;
        }
        if (_input.restart)
        {
            UiController.instance.Restart();
        }
        if (_input.pause)
        {
            UiController.instance.PauseClicked();
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
            inteText = "(F) Light Fire";
            UiController.instance.changeInsText(inteText);
            inteCode = 2;
            targetObject = other.gameObject;
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
        else if (other.tag == "campFire")
        {
            inteText = "(F) Set Fire";
            UiController.instance.changeInsText(inteText);
            inteCode = 2;
            interactable = true;
            targetObject = other.gameObject;
        }
        else if (other.tag == "radio")
        {
            StartCoroutine(UiController.instance.stopRadio());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        wrapUp();
    }

    public void setFire(GameObject targetObject)
    {
        FireAnimation.instance.StartFireAnim();
    }
    public void loopInventory()
    {
        for(int i = 0; i < 12; i++)
        {
            if(i < playerBag.GetSize())
            {
                UiController.instance.printInventory(playerBag.GetItem(i).itemType, i, playerBag.GetItem(i).amount);
                UiController.instance.Selectable(true, i);
            }
            else
            {
                UiController.instance.printInventory(items.ItemType.Empty, i, 0);
                UiController.instance.Selectable(false, i);
            }
        }
    }
    public void wrapUp()
    {
        _input.interact = false;
        interactable = false;
        UiController.instance.changeInsText("");
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
                playerBag.AddItem(PresetItems.instance.pot);
                break;
            case "gamCon":
                playerBag.AddItem(PresetItems.instance.gamCon);
                break;
            case "bat":
                playerBag.AddItem(PresetItems.instance.bat);
                break;
            case "vase":
                playerBag.AddItem(PresetItems.instance.vase);
                break;
            case "book":
                playerBag.AddItem(PresetItems.instance.book);
                break;
            case "knife":
                playerBag.AddItem(PresetItems.instance.knife);
                break;
        }
        loopInventory();
        wrapUp();
        Destroy(target);
    }

    public void DropItem(bool spawn)
    {
        int selection = UiController.instance.itemSelected;
        if (UiController.instance.itemSelected >= 0 &&
            UiController.instance.itemSelected < playerBag.AllItem.Count())
        {
            if (spawn)
            {
                GameObject newItem = Instantiate(playerBag.AllItem[UiController.instance.itemSelected].Get3dGameObject()
                    , dropPoint.transform.position, dropPoint.transform.rotation);
            }
            
            playerBag.AllItem[selection].amount--;
            if (playerBag.AllItem[selection].amount <= 0)
            {
                playerBag.AllItem.Remove(playerBag.AllItem[selection]);
                UiController.instance.dropButton.SetActive(false);
            }
            loopInventory();
        }
    }

    public void openBox(GameObject target)
    {
        GameObject item = Instantiate(BoxManager.instance.boxItems[cont], target.transform.position,Quaternion.identity);
        cont++;
        wrapUp();
        Destroy(target);
    }
}


