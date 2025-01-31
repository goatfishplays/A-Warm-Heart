using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    private Vector2 startLoc;
    public Vector2 endLocAdd;
    public float secs = 1f;
    public float curTime = 0f;
    public bool sin = false;
    // Start is called before the first frame update
    void Start()
    {
        startLoc = transform.position;
        endLocAdd += startLoc;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime / secs;
        transform.position = Vector2.Lerp(startLoc, endLocAdd, sin ? Mathf.Sin(Mathf.Min(1, curTime) * Mathf.PI * 0.5f) : curTime);
    }
}
