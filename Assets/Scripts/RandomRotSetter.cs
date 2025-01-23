using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotSetter : MonoBehaviour
{
    public float minRotBound = -22.5f;
    public float maxRotBound = 22.5f;
    // Start is called before the first frame update
    void Awake()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, Random.Range(minRotBound, maxRotBound)));
    }
}
