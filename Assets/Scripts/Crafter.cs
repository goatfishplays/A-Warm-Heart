using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour
{
    [System.Serializable]
    public class Recipe
    {
        public Inventory.Item[] ingreds;
        public SpawnerSO spawnerSO;
        // public string partType; 
        // public int partInd;
        public Recipe(Inventory.Item[] ingreds, SpawnerSO spawnerSO = null)
        {
            this.ingreds = ingreds;
            // this.partType = partType;
            this.spawnerSO = spawnerSO;
        }
    }

    public BodyManager bm;
    public Inventory inv;
    public Entity playerEntity;
    public Button craftButton;
    public IngredController[] ingredsUI;
    public string[] recipesBuilderNames;
    public Recipe[] recipesBuilder;
    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
    public string curRecipe = "";
    public TextMeshProUGUI desc;
    // public string partType = "";

    public void Awake()
    {
        for (int i = 0; i < recipesBuilderNames.Length; i++)
        {
            recipes.Add(recipesBuilderNames[i], recipesBuilder[i]);
        }
        // print(recipes["Rifle"].spawnerSO.spawnerName);
    }

    public void ReadRecipe(string recName)
    {

        bool craftable = true;
        if (bm.selectedPart == null || (bm.selectedPart.GetComponent<Spawner>().spawnerBase != null && recName == bm.selectedPart.GetComponent<Spawner>().spawnerBase.spawnerName))
        {
            craftable = false;
        }
        foreach (IngredController ic in ingredsUI)
        {
            ic.gameObject.SetActive(false);
        }
        // print(recName);
        if (recipes.ContainsKey(recName))
        {
            curRecipe = recName;


            // print("hats");
            Recipe rec = recipes[recName];

            desc.text = rec.spawnerSO.description;

            for (int i = 0; i < rec.ingreds.Length; i++)
            {
                ingredsUI[i].gameObject.SetActive(true);
                ItemSO curItem = rec.ingreds[i].itemSO;
                int count = rec.ingreds[i].count;
                bool enough = inv.CheckItem(curItem, count);
                if (!enough)
                {
                    craftable = false;
                }
                ingredsUI[i].SetBois(curItem.itemName, "x" + count, curItem.icon, enough ? Color.white : Color.red);
            }
        }
        else
        {
            craftable = false;
        }
        craftButton.interactable = craftable;
    }

    public void Craft()
    {
        Recipe rec = recipes[curRecipe];
        if (rec.spawnerSO != null)
        {
            // bm.selectedPart.GetComponent<Spawner>().spawnerBase = rec.spawnerSO; 
            bm.SetSpawner(bm.selectedPart.GetComponent<Spawner>(), rec.spawnerSO);
            if (curRecipe == "Steel Heart")
            {
                Healthbar.instance.SetAlternateHeart();
            }
        }


        for (int i = 0; i < rec.ingreds.Length; i++)
        {
            ItemSO curItem = rec.ingreds[i].itemSO;
            int count = rec.ingreds[i].count;
            inv.RemoveItem(curItem, count);
        }
        ReadRecipe(curRecipe);
        // if (rec.partType == "Arm")
        // switch (rec.partType)
        // {
        //     case "Spawner":
        //         break;
        // }
    }



    // private void Start()
    // {
    //     ReadRecipe(recipes[0]);
    // }

}
