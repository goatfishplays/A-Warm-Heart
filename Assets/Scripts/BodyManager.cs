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

    public void SetSpawner(Spawner spawner, SpawnerSO spawnerSO)
    {
        spawner.spawnerBase = spawnerSO;
        spawner.GetComponent<SpriteRenderer>().sprite = spawnerSO.sprite;
    }





}
