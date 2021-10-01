using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipes : MonoBehaviour
{
    public static CraftRecipes instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Recipe> craftRecipes = new List<Recipe>();
}
