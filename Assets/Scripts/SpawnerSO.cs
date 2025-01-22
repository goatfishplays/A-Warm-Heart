using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Spawner", menuName = "Spawner")]
public class SpawnerSO : ScriptableObject
{
    public string spawnerName = "No Name";
    public string description = "No Description";
    public Sprite sprite;
    public int spawnerID = -1;

    [Header("Spawn Info")]
    public GameObject bulletPrefab;
    public bool rotateBullets = false;
    public float bulletRotOffset = 0f;
    public float rotForShotOffset = 0f;
    public float bulletShotForce = 5f;
    public bool parrentToSpawner = false;
    public int maxTicksBetweenSpawns = 20;
    public bool destroyAfterDisable = true;
    public bool unqueueAfterShot = false;

}
