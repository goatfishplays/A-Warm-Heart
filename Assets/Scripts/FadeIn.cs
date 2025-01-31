using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool started = false;
    public float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // sr.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            Color temp = Color.white;
            temp.a = sr.color.a + Time.deltaTime / time;
            sr.color = temp;
        }
    }
}
