using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{

    public float rotSpeed = 720f;
    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.forward * (transform.eulerAngles.z + Time.deltaTime * rotSpeed));
        transform.Rotate(Vector3.forward * (Time.deltaTime * rotSpeed));
    }
}
