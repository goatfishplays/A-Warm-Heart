using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredController : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;
    public Image image;
    public void SetBois(string name, string count, Sprite icon, Color setColor)
    {
        nameText.text = name;
        countText.text = count;
        image.sprite = icon;
        nameText.color = setColor;
        countText.color = setColor;
        image.color = setColor;
    }
}
