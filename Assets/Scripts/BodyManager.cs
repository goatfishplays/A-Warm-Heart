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
    public bool coreChanged = false;
    public bool heartChanged = false;

    public bool footLChanged = false;
    public bool legLChanged = false;
    public bool armLChanged = false;
    public bool handLChanged = false;
    public bool shoulderLChanged = false;
    public bool eyeLChanged = false;

    public bool footRChanged = false;
    public bool legRChanged = false;
    public bool armRChanged = false;
    public bool handRChanged = false;
    public bool shoulderRChanged = false;
    public bool eyeRChanged = false;

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
        if (selectedPart == core)
        {
            coreChanged = true;

        }
        if (selectedPart == heart)
        {
            heartChanged = true;

        }

        if (selectedPart == footL)
        {
            footLChanged = true;

        }
        if (selectedPart == legL)
        {
            legLChanged = true;

        }
        if (selectedPart == armL)
        {
            armLChanged = true;

        }
        if (selectedPart == handL)
        {
            handLChanged = true;

        }
        if (selectedPart == shoulderL)
        {
            shoulderLChanged = true;

        }
        if (selectedPart == eyeL)
        {
            eyeLChanged = true;

        }

        if (selectedPart == footR)
        {
            footRChanged = true;

        }
        if (selectedPart == legR)
        {
            legRChanged = true;

        }
        if (selectedPart == armR)
        {
            armRChanged = true;

        }
        if (selectedPart == handR)
        {
            handRChanged = true;

        }
        if (selectedPart == shoulderR)
        {
            shoulderRChanged = true;

        }
        if (selectedPart == eyeR)
        {
            eyeRChanged = true;
        }
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
