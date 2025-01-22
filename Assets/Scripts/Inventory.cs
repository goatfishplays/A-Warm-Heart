using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public ItemSO itemSO;
        public int count;
        public Item(ItemSO itemSO, int count)
        {
            this.itemSO = itemSO;
            this.count = count;
        }
    }
    public List<Item> items = new List<Item>();

    public Entity entity;



    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
    }


    public void AddItem(ItemSO itemSO, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemSO == itemSO)
            {
                items[i].count += amount;
                return;
            }
        }
        items.Add(new Item(itemSO, amount));
    }

    public bool CheckItem(ItemSO itemSO, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemSO == itemSO)
            {
                return items[i].count >= amount;
            }
        }
        return false;
    }


    // Assumes you already checked for count

    public void RemoveItem(ItemSO itemSO, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemSO == itemSO)
            {
                items[i].count -= amount;
                if (items[i].count <= 0)
                {
                    items.RemoveAt(i);
                }
                return;
            }
        }
    }

}
