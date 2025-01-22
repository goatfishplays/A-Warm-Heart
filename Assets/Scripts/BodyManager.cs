using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{


    public GameObject core;

    public GameObject footL;
    public GameObject legL;
    public GameObject armL;
    public GameObject handL;
    public GameObject shoulderL;

    public GameObject footR;
    public GameObject legR;
    public GameObject armR;
    public GameObject handR;
    public GameObject shoulderR;


    public void Aim(GameObject aimObj, Vector2 targ)
    {
        aimObj.transform.right = targ;
        aimObj.GetComponent<Spawner>().Aim(targ);
    }

    public void Fire(GameObject fireObj)
    {
        fireObj.GetComponent<Spawner>().SetShotQueue(true);
    }




}
