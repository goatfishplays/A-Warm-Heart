using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnChance
    {
        public GameObject[] spawnPool;
        public int rolls;
        public float spawnChance;
        public Vector2 spawnXRange;
        public Vector2 spawnYRange;
        public bool isEntity;
    }

    [System.Serializable]
    public struct Level
    {
        public SpawnChance[] spawns;

    }

    public enum LevelState
    {
        EnemiesAlive,
        EnemiesDead,
        NextLevelQueued,
        Upgradeing,
        NextLevelSpawning
    }

    public static LevelManager instance;
    [Header("Objects")]
    public Transform entitiesHolder;
    public Transform bgObjectHolder;
    public Transform dropHolder;
    public Transform telePoles;
    public Vector2 telePolesRange = new Vector2(-10, 10);
    public Transform car;
    public float carStartPos;
    public float carWaitPos;
    public float carExitPos;
    private Vector3 carTarg;
    public float carTime = 0f;
    public float carPullUpTime = 2f;
    public float carExitTime = 1f;
    public GameObject carWaitingArea;
    public float carRad = 3f;
    public Transform player;
    public SpriteRenderer screenHider;
    [Header("Levels")]
    public GameObject lastLevel;
    public Level[] levels;
    public MenuManager menuManager;

    [Header("Cur State")]
    public int levelInd = 0;
    public LevelState levelState = LevelState.NextLevelQueued;




    // Start is called before the first frame update
    void Start()
    {
        carTarg = car.position;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (levelState)
        {
            case LevelState.EnemiesAlive:
                Color temp = screenHider.color;
                if (temp.a > 0)
                {
                    temp.a -= Time.deltaTime * 5;
                }
                screenHider.color = temp;
                if (entitiesHolder.childCount == 0)
                {
                    levelState = LevelState.EnemiesDead;
                    carTime = 0;
                    carTarg.x = carWaitPos;
                }
                break;
            case LevelState.EnemiesDead:

                Color temp2 = screenHider.color;
                if (temp2.a > 0)
                {
                    temp2.a -= Time.deltaTime * 5;
                }
                screenHider.color = temp2;
                if (carTime < 1f)
                {
                    carTime += Time.deltaTime / carPullUpTime;
                    car.position = Vector3.Lerp(new Vector3(carStartPos, car.position.y), carTarg, Mathf.Sin(carTime * Mathf.PI / 2f));
                }
                else
                {
                    carWaitingArea.SetActive(true);
                    if (PlayerControl.instance.pInputs.Player.UpgradeMenu.WasPressedThisFrame() && (player.position - car.position).magnitude < carRad)
                    {
                        carWaitingArea.SetActive(false);
                        levelState = LevelState.NextLevelQueued;
                        player.position = Vector3.one * 100;
                        carTime = 0f;
                        carTarg.x = 2 * carExitPos - carWaitPos;
                        // carTarg.x = carExitPos; 
                    }
                }
                break;
            case LevelState.NextLevelQueued:
                if (carTime < 0.5f)
                {
                    carTime += Time.deltaTime / carExitTime;
                    car.position = Vector3.Lerp(new Vector3(carWaitPos, car.position.y), carTarg, Mathf.Sin(carTime * Mathf.PI));
                    Color tempc = screenHider.color;
                    tempc.a = 2 * carTime;
                    screenHider.color = tempc;
                }
                else
                {
                    levelInd++;
                    levelState = LevelState.Upgradeing;
                    menuManager.ToggleUpgradeMenu(true);
                    WipeChildren(entitiesHolder);
                    WipeChildren(bgObjectHolder);
                    WipeChildren(dropHolder);
                    Vector3 telePos = telePoles.position;
                    telePos.x = Random.Range(telePolesRange.x, telePolesRange.y);
                    telePoles.position = telePos;
                    carTarg.x = carStartPos;
                    car.position = carTarg;
                    player.position = car.position + Vector3.down + Vector3.right * 0.5f;
                    player.GetComponent<Entity>().ChangeHealth(25, false, true);
                }
                break;
            case LevelState.Upgradeing:
                // Upgrades Menu
                if (PlayerControl.instance.pInputs.Player.UpgradeMenu.WasPressedThisFrame())
                {
                    levelState = LevelState.NextLevelSpawning;
                }
                break;
            case LevelState.NextLevelSpawning:
                if (levelInd < levels.Length)
                {
                    Level curLev = levels[levelInd];
                    for (int i = 0; i < curLev.spawns.Length; i++)
                    {
                        SpawnChance curSpawning = curLev.spawns[i];
                        for (int j = 0; j < curSpawning.rolls; j++)
                        {
                            if (Random.value <= curSpawning.spawnChance)
                            {
                                Instantiate(curSpawning.spawnPool[Random.Range(0, curSpawning.spawnPool.Length)],
                                 new Vector2(Random.Range(curSpawning.spawnXRange.x, curSpawning.spawnXRange.y), Random.Range(curSpawning.spawnYRange.x, curSpawning.spawnYRange.y)),
                                 Quaternion.identity,
                                 curSpawning.isEntity ? entitiesHolder : bgObjectHolder);
                            }
                        }
                    }
                }
                else
                {
                    Instantiate(lastLevel);
                }
                menuManager.ToggleUpgradeMenu(false);
                levelState = LevelState.EnemiesAlive;
                break;
        }

    }


    public void WipeChildren(Transform obj)
    {
        foreach (Transform t in obj)
        {
            Destroy(t.gameObject);
        }
    }
}
