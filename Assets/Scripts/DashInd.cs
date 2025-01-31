using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashInd : MonoBehaviour
{
    public Image dashIndImg;
    public Entity playerEntity;
    public Color semiActive = Color.white;
    public Color inactiveColor = Color.grey;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerEntity.maxDashes == 0)
        {
            dashIndImg.color = Color.clear;
        }
        else if (playerEntity.dashes == 0)
        {
            dashIndImg.color = inactiveColor;
        }
        else if (playerEntity.dashes == 1)
        {
            dashIndImg.color = semiActive;
        }
        else
        {
            dashIndImg.color = Color.white;
        }
    }
}
