using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inv;
    public IngredController[] ingredsUI;

    public void UpdateMenu()
    {
        foreach (IngredController ic in ingredsUI)
        {
            ic.gameObject.SetActive(false);
        }
        for (int i = 0; i < inv.items.Count; i++)
        {
            ingredsUI[i].gameObject.SetActive(true);
            ItemSO curItem = inv.items[i].itemSO;
            int count = inv.items[i].count;

            ingredsUI[i].SetBois(curItem.itemName, "x" + count, curItem.icon, Color.white);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMenu();
    }
}
