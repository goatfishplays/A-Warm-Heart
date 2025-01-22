using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public Spawner[] spawnersListRot;
    public Spawner[] spawnersListNoRot;


    public void FireAll()
    {
        foreach (Spawner spawner in spawnersListRot)
        {
            spawner.SetShotQueue(true);
        }
        foreach (Spawner spawner in spawnersListNoRot)
        {
            spawner.SetShotQueue(true);
        }
    }

    public void AimAll(Vector2 targ)
    {
        foreach (Spawner spawner in spawnersListRot)
        {
            spawner.Aim(targ, true, true);
        }
        foreach (Spawner spawner in spawnersListNoRot)
        {
            spawner.Aim(targ, false);
        }
    }

}
