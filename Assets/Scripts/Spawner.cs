using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Owner")]
    public int ownerID = -1;

    [Header("Spawn Info")]
    public SpawnerSO spawnerBase;
    public float rotForShot = 0f;
    public int ticksTillNextSpawn = 0;
    public int numShotsTillDisable = -1;
    // public float offset = 0;
    public Vector2 offset = Vector2.right;
    [Header("Spawner Aiming")]

    public bool active = true;
    // public bool followsCursor = true;
    protected Spawner[] childrenSpawners;
    public bool shotQueued = false;

    public void SetShotQueue(bool set = true)
    {
        shotQueued = set;
        foreach (Spawner ts in childrenSpawners)
        {
            if (ts != this)
            {
                ts.shotQueued = set;
            }
        }
    }

    protected virtual void Awake()
    {
        childrenSpawners = GetComponentsInChildren<Spawner>();
        foreach (Spawner ts in childrenSpawners)
        {
            if (ts != this)
            {
                ts.ownerID = ownerID;
            }
        }
    }

    public virtual void Aim(Vector2 pos, bool setsTransRot = false, bool setTransRotDown = false)
    {
        foreach (Spawner ts in childrenSpawners)
        {
            if (ts != this)
            {
                ts.Aim(pos);
            }
        }
        Vector2 dir = pos - (Vector2)transform.position;

        rotForShot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (setsTransRot)
        {
            if (!setTransRotDown)
            {
                transform.right = dir;
            }
            else
            {
                transform.up = -dir;
            }
        }
    }

    // public virtual void Click(int click)
    // {
    //     foreach (Spawner ts in childrenSpawners)
    //     {
    //         if (ts != this)
    //         {
    //             ts.Click(click);
    //         }
    //     }
    //     if (click > 1)
    //     {
    //         active = true;
    //     }
    // }

    public virtual void FixedUpdate()
    {
        if (active)
        {
            if (ticksTillNextSpawn == 0)
            {
                Spawn();
            }
            else if (shotQueued)
            {
                ticksTillNextSpawn--;
            }

        }
        // else
        // {
        //     TimedSpawner parent = GetComponentInParent<TimedSpawner>();
        //     if (parent != null && parent.active)
        //     {
        //         active = true;
        //     } 
        // }

    }

    public virtual void Spawn()
    {
        if (spawnerBase.unqueueAfterShot)
        {
            shotQueued = false;
        }

        GameObject curBullet = Instantiate(spawnerBase.bulletPrefab, transform.position + transform.TransformDirection(offset), Quaternion.Euler(0, 0, spawnerBase.bulletRotOffset));
        curBullet.GetComponentInChildren<Attack>(true).ownerID = ownerID;
        if (spawnerBase.rotateBullets)
        {
            curBullet.transform.rotation = Quaternion.Euler(0, 0, spawnerBase.bulletRotOffset + rotForShot + spawnerBase.rotForShotOffset);
        }
        if (spawnerBase.parrentToSpawner)
        {
            curBullet.transform.parent = transform;
        }
        if (spawnerBase.bulletShotForce != 0)
        {
            Rigidbody2D rb = curBullet.GetComponent<Rigidbody2D>();
            // apply directional velocity 
            rb.AddForce(new Vector2(Mathf.Cos(Mathf.Deg2Rad * rotForShot + spawnerBase.rotForShotOffset), Mathf.Sin(Mathf.Deg2Rad * rotForShot + spawnerBase.rotForShotOffset)).normalized * spawnerBase.bulletShotForce, ForceMode2D.Impulse);

        }

        ResetSpawner();
    }

    public virtual void ResetSpawner()
    {

        #region Destruction 
        numShotsTillDisable--;
        if (numShotsTillDisable == 0)
        {
            if (spawnerBase.destroyAfterDisable)
            {
                Destroy(gameObject);
            }
            active = false;
        }
        #endregion

        ticksTillNextSpawn = spawnerBase.maxTicksBetweenSpawns;
    }

}
