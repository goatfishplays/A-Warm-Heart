using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public float viewTimeMax = 5f;
    public float viewTime = 5f;
    public SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        viewTime -= Time.deltaTime;
        transform.localScale = Vector3.one * (1 - viewTime / viewTimeMax);
        sr.color = new Color(1, 1, 1, viewTime / viewTimeMax);
        if (viewTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
