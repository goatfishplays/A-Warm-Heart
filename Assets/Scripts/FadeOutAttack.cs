using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeOutAttack : MonoBehaviour
{
    public SpriteRenderer sr;
    public Attack atk;
    // public float temp = 5f; 

    // private void Start()
    // {
    //     sr = GetComponent<SpriteRenderer>();
    //     atk = GetComponent<Attack>(); 
    // }

    private void Update()
    {
        Color tc = sr.color;
        // float temp = atk.liveTime / atk.attackBase.liveTime;

        // float aVal = (Mathf.Exp(temp * atk.liveTime) - 1) / Mathf.Exp(temp * atk.attackBase.liveTime);
        // float aVal = Mathf.Sqrt(atk.liveTime / atk.attackBase.liveTime); 
        float aVal = atk.liveTime / atk.attackBase.liveTime;
        // tc.a = Mathf.Exp(5 * temp) / (1 + );
        tc.a = aVal;
        sr.color = tc;
    }
}
