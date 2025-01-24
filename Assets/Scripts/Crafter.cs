using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    public BodyManager bm;
    public Inventory inv;

    [System.Serializable]
    public class Recipe
    {
        public Inventory.Item[] ingreds;
        public string partType;
        public int partInd;
        public Recipe(Inventory.Item[] ingreds, string partType, int partInd)
        {
            this.ingreds = ingreds;
            this.partType = partType;
            this.partInd = partInd;
        }
    }

    public IngredController[] ingredsUI;
    public Recipe[] recipes;
    public string partType = "";

    public void ReadRecipe(Recipe rec)
    {
        foreach (IngredController ic in ingredsUI)
        {
            ic.gameObject.SetActive(false);
        }
        for (int i = 0; i < rec.ingreds.Length; i++)
        {
            ingredsUI[i].gameObject.SetActive(true);
            ItemSO curItem = rec.ingreds[i].itemSO;
            int count = rec.ingreds[i].count;

            ingredsUI[i].SetBois(curItem.itemName, "x" + count, curItem.icon, inv.CheckItem(curItem, count) ? Color.white : Color.red);
        }
    }



    private void Start()
    {
        ReadRecipe(recipes[0]);
    }

}
