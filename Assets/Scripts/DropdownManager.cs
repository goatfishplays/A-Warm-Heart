using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Crafter crafter;
    public List<TMP_Dropdown.OptionData> brains;
    public List<TMP_Dropdown.OptionData> hearts;
    public List<TMP_Dropdown.OptionData> eyes;
    public List<TMP_Dropdown.OptionData> shoulders;
    public List<TMP_Dropdown.OptionData> arms;
    public List<TMP_Dropdown.OptionData> hands;
    public List<TMP_Dropdown.OptionData> legs;
    public List<TMP_Dropdown.OptionData> feet;

    public void UpdateDDOptions(string type)
    {
        dropdown.ClearOptions();
        switch (type)
        {
            case "Brain":
                dropdown.AddOptions(brains);
                break;
            case "Heart":
                dropdown.AddOptions(hearts);
                break;
            case "Eye":
                dropdown.AddOptions(eyes);
                break;
            case "Shoulder":
                dropdown.AddOptions(shoulders);
                break;
            case "Arm":
                dropdown.AddOptions(arms);
                break;
            case "Hand":
                dropdown.AddOptions(hands);
                break;
            case "Leg":
                dropdown.AddOptions(legs);
                break;
            case "Foot":
                dropdown.AddOptions(feet);
                break;
            default:
                break;
        }

        crafter.ReadRecipe(dropdown.captionText.text);
    }

    public void UseSelection()
    {
        crafter.ReadRecipe(dropdown.captionText.text);
    }

    private void Start()
    {
        UpdateDDOptions("");
        // UpdateDDOptions("Hand");
        // UpdateDDOptions("");
        // UpdateDDOptions("Hand");
    }
}


