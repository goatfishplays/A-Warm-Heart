using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{

    public GameObject core;

    public GameObject heart;

    public GameObject footL;
    public GameObject legL;
    public GameObject armL;
    public GameObject handL;
    public GameObject shoulderL;
    public GameObject eyeL;

    public GameObject footR;
    public GameObject legR;
    public GameObject armR;
    public GameObject handR;
    public GameObject shoulderR;
    public GameObject eyeR;

    public Spawner[] spawners;
    public Entity entity;

    public GameObject selectedPart;
    private void Start()
    {
        UpdateEntityStates();
    }
    public void UpdateEntityStates()
    {
        entity.maxDashes = 0;
        entity.maxShield = 0;
        entity.wheelSpeedModifier = 0;
        entity.steelHeartUnlocked = false;
        entity.healthChangeRate = 1;
        entity.shield.SetActive(false);
        if (GetComponent<PlayerControl>())
        {
            GetComponent<PlayerControl>().numDropRolls = 1;
        }


        foreach (Spawner spawner in spawners)
        {
            if (spawner.spawnerBase != null)
            {
                switch (spawner.spawnerBase.spawnerName)
                {
                    case "Steel Heart":
                        entity.steelHeartUnlocked = true;
                        entity.healthChangeRate *= 2;
                        break;
                    case "Booster Leg":
                        entity.maxDashes++;
                        entity.dashes++;
                        break;
                    case "Wheel":
                        entity.wheelSpeedModifier += .25f;
                        break;
                    case "Shield Projector":
                        entity.shield.SetActive(false);
                        entity.maxShield++;
                        entity.curShield++;
                        entity.shield.SetActive(true);
                        break;
                    case "Scanner Eye":
                        if (GetComponent<PlayerControl>())
                        {
                            GetComponent<PlayerControl>().numDropRolls++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }



        if (entity.dashes > entity.maxDashes)
        {
            entity.dashes = entity.maxDashes;
        }
        if (entity.curShield > entity.maxShield)
        {
            entity.curShield = entity.maxShield;
        }
    }

    public void SetSel(GameObject setSel)
    {
        selectedPart = setSel;
    }

    public void SetSpawner(Spawner spawner, SpawnerSO spawnerSO, bool upEntState = true)
    {

        spawner.spawnerBase = spawnerSO;
        spawner.GetComponent<SpriteRenderer>().sprite = spawnerSO.sprite;
        if (upEntState)
        {
            UpdateEntityStates();
        }
    }

    public void SetRandom(string partPart, SpawnerSO spawnerSO)
    {
        bool right = Random.value > 0.5f;
        switch (partPart)
        {
            case "Arm":
                if (right)
                {
                    SetSpawner(armR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(armL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Hand":
                if (right)
                {
                    SetSpawner(handR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(handL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Shoulder":
                if (right)
                {
                    SetSpawner(shoulderR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(shoulderL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Leg":
                if (right)
                {
                    SetSpawner(legR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(legL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Foot":
                if (right)
                {
                    SetSpawner(footR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(footL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Eye":
                if (right)
                {
                    SetSpawner(eyeR.GetComponent<Spawner>(), spawnerSO, false);
                }
                else
                {
                    SetSpawner(eyeL.GetComponent<Spawner>(), spawnerSO, false);
                }
                break;
            case "Heart":
                SetSpawner(heart.GetComponent<Spawner>(), spawnerSO, false);
                break;
        }
        UpdateEntityStates();
    }





}
