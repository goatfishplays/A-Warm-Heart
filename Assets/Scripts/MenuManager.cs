using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject UpgradesMenu;

    public void ToggleUpgradeMenu()
    {
        UpgradesMenu.SetActive(!UpgradesMenu.activeSelf);
        if (UpgradesMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
