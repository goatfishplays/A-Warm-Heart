using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public Spawner[] spawnersListRot;
    public Spawner[] spawnersListNoRot;
    // public int fireBufferHoldLengthMax = 5;
    // public int fireBufferHoldLengthCur = 0;




    public void FireAll(bool setTrue = true)
    {
        // fireBufferHoldLengthCur = fireBufferHoldLengthMax;

        foreach (Spawner spawner in spawnersListRot)
        {
            spawner.SetShotQueue(setTrue);
        }
        foreach (Spawner spawner in spawnersListNoRot)
        {
            spawner.SetShotQueue(setTrue);
        }
    }

    public void AimAll(Vector2 targ)
    {
        foreach (Spawner spawner in spawnersListRot)
        {
            if (spawner != null)
            {
                spawner.Aim(targ, true, true);
            }
        }
        foreach (Spawner spawner in spawnersListNoRot)
        {
            if (spawner != null)
            {
                spawner.Aim(targ, false);
            }
        }
    }

    private void FixedUpdate()
    {

    }

}
