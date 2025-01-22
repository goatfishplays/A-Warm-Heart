using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName = "No Name";
    public string description = "No Description";
    public string useText = "No use text";
    public Sprite icon;
    public int itemID = -1;

}
