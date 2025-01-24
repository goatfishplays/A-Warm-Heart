using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
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
            case "Hand":
                dropdown.AddOptions(hands);
                break;
            case "Leg":
                dropdown.AddOptions(legs);
                break;
            default:
                dropdown.options = brains;
                break;
        }
    }
}
