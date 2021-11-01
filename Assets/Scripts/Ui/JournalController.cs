using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public static JournalController instance;
    private string page1Info;
    private string page2Info;
    private List<(string, string)> contextList = new List<(string, string)>();
    [SerializeField] Text page1Text;
    [SerializeField] Text page2Text;
    private int pageNumber = 1;
    private int maxPage = 2;

    private string dayOneText = "day 1";
    private string dayTwoText = "day 2";
    private string dayThreeText = "day 3";
    private string dayFourText = "day 4";

    private void Awake()
    {
        instance = this;
    }

    public GameObject journalPanel;
    
    public void LoadInfo()
    {
        contextList.Add((dayOneText, dayTwoText));
        contextList.Add((dayThreeText, dayFourText));
    }

    public void LoadPage()
    {
        page1Text.text = contextList[pageNumber - 1].Item1;
        page2Text.text = contextList[pageNumber - 1].Item2;
    }

    public void ShowJournal()
    {
        journalPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextPage()
    {
        if (pageNumber < maxPage)
        {
            pageNumber++;
            LoadPage();
        }
    }

    public void PreviousPage()
    {
        if (pageNumber > 1)
        {
            pageNumber--;
            LoadPage();
        }
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
