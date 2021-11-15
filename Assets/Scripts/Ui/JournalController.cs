using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public static JournalController instance;
    private List<(string, string)> contextList = new List<(string, string)>();
    public Dictionary<string, Recipe> recipesIGot = new Dictionary<string, Recipe>();
    public List<Recipe> preloadedRecipes = new List<Recipe>();
    [SerializeField] Text page1Text;
    [SerializeField] Text page2Text;
    private int pageNumber = 1;
    private int maxPage = 2;
    private string deathReason = "";

    private string dayOneText = 
        "day 1, Sunny\n\n" +
        "I can't believe this actaully happened, I'm the only survivor. " +
        "I should find some rock and wood to make a campfire.\n\n" +
        "I hit the palm tree with a rock, coconut dropped. Those should keep me hydrated.";
    private string dayTwoText = 
        "day 2, Rainy\n\n" +
        "I found a pot in the delivery box, I put it on the ground and collected rain water with it, lucky me.\n\n" +
        "I cooked some mushroom with the campfire, it's the most delicious thing.\n\n" +
        "I am full, I think I have the energy to search for more resources";
    private string dayThreeText = "day 3, Cloudy\n\n" +
        "A helicopter came, but they didn't see me. " +
        "I hope they come back because I made this huge fire, should draw their attention.\n\n" +
        "I made a Axe with a skate shoe I found and a wood branch, it made cutting tree much easier.";
    private string dayFourText = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject journalPanel;

    private void Start()
    {
        journalPanel.SetActive(false);
        foreach (Recipe recipe in preloadedRecipes)
        {
            recipesIGot.Add(recipe.name, recipe);
        }
    }

    public void LoadInfo()
    {
        contextList.Clear();
        LoadDeathReason();
        contextList.Add((dayOneText, dayTwoText));
        contextList.Add((dayThreeText, dayFourText));
        LoadRecipe();
    }

    public void LoadRecipe()
    {
        maxPage = 2;
        if (recipesIGot.Count != 0)
        {
            maxPage += (int)Mathf.Ceil(recipesIGot.Count / 6f);
        }

        int recipeCounter = 1;
        string page1 = "";
        string page2 = "";
        int i = 0;
        foreach (var recipe in recipesIGot)
        {
            if (recipeCounter <= 4)
            {
                page1 += WriteRecipe(recipe.Value);
                recipeCounter++;
            }
            else if (recipeCounter > 4 && recipeCounter <= 8)
            {
                page2 += WriteRecipe(recipe.Value);
                recipeCounter++;
            }
            else if (recipeCounter > 8)
            {
                contextList.Add((page1, page2));
                recipeCounter = 1;
                page1 = "";
                page2 = "";
            }
            i++;
            if (i == recipesIGot.Count)
            {
                contextList.Add((page1, page2));
            }
        }
    }

    private string WriteRecipe(Recipe recipe)
    {
        string res = "";
        if (recipe.isCookRecipe)
        {
            res += "(Cook) ";
        }

        for (int i = 0; i < recipe.craftMaterials.Count; i++)
        {
            if (i + 1 < recipe.craftMaterials.Count)
            {
                res += recipe.GetMaterialName(i) + " + ";
            }
            else
            {
                res += recipe.GetMaterialName(i) + " = " + recipe.GetResultName() + "\n\n";
            }
        }
        return res;
    }

    public void LoadDeathReason()
    {
        string reason = CollectFeedback.instance.GetDeathReason();
        if (reason == "")
        {
            dayFourText = "day 4, Sunny\n\n" +
            "The helicopter never came back, I don't think I can make it.\n\n" +
            "I should make a boat with all the wood I got, should take a risk....\n\n" +
            "If anyone see this journal, my name is Polly, please bury me.";
        }
        else
        {
            dayFourText = "day 4, Sunny\n\n" +
            "The helicopter never came back, I don't think I can make it.\n\n" +
            "I should make a boat with all the wood I got, should take a risk....\n\n" +
            "I am " + CollectFeedback.instance.GetDeathReason() + "\n\n" +
            "If anyone see this journal, my name is Polly, please bury me.";
        }
    }

    public void LoadPage()
    {
        page1Text.text = contextList[pageNumber - 1].Item1;
        page2Text.text = contextList[pageNumber - 1].Item2;
        Debug.Log("context list: " + contextList.Count);
        Debug.Log("recipe I got: " + recipesIGot.Count);
    }

    public void ShowJournal()
    {
        playerCollider.instance.bagOn = true;
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
        playerCollider.instance.bagOn = false;
        journalPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
