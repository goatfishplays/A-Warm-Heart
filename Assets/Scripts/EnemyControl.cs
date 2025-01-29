using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public enum EntityState
    {
        Wander,
        Attack,
        Pursuit,
        Idle
    }

    public Entity entity;
    public bool facePlayer = true;
    public float[] stateChances = { 0.50f, 0.35f, 0f, .15f };

    public EntityState entityState;

    [Header("Wander")]
    public Vector2 wandPos = Vector2.zero;
    public Vector2 xyMax = new Vector2(10, 7.5f);
    public float minWandDist = 1f;
    public float maxWandDist = 10f;
    // public float playerAvoidRad = 3f;
    public bool swapToAtkAfterWand = false;

    [Header("Attack")]
    public float minFireTime = .1f;
    public float maxFireTime = 3f;
    public bool swapWhileAttacking = false;
    public float fireTime = 0f;
    public SpawnerManager spawnerManager;

    [Header("Pursuit")]
    public float minPursTime = .1f;
    public float maxPursTime = 3f;
    public float pursTime = 0f;
    public float pursStopDistance = 1f;
    public bool swapToAtkAfterPurs = false;

    [Header("Idle")]
    public float minIdleTime = .1f;
    public float maxIdleTime = 3f;
    public float idleTime = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (facePlayer)
        {
            if (transform.position.x > PlayerControl.instance.transform.position.x)
            {
                transform.eulerAngles = Vector3.up * 180;
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (entityState == EntityState.Wander)
        {
            entity.Move(wandPos - (Vector2)transform.position);
            // End Condition
            if (Mathf.Abs(((Vector2)transform.position - wandPos).magnitude) < .1f)
            {
                // SetState(EntityState.Wander); // CHANGE THIS TO ROLL LMMAO
                if (swapToAtkAfterWand)
                {
                    SetState(EntityState.Attack);
                }
                else
                {
                    RollState();
                }
            }
        }

        if (entityState == EntityState.Pursuit)
        {
            entity.Move(PlayerControl.instance.transform.position - transform.position);
            pursTime -= Time.deltaTime;
            if (pursTime < 0f || Mathf.Abs(((Vector2)transform.position - (Vector2)PlayerControl.instance.transform.position).magnitude) < pursStopDistance)
            {
                if (swapToAtkAfterPurs)
                {
                    SetState(EntityState.Attack);
                }
                else
                {
                    RollState();
                }
            }
        }

        if (entityState == EntityState.Idle)
        {
            entity.Move(Vector2.zero);
            idleTime -= Time.deltaTime;
            if (idleTime < 0)
            {
                RollState();
            }
        }

        if (entityState == EntityState.Attack)
        {
            entity.Move(Vector2.zero);
        }

        if (fireTime > 0)
        {
            spawnerManager.AimAll(PlayerControl.instance.transform.position);
            spawnerManager.FireAll();
            fireTime -= Time.deltaTime;
        }
        else if (entityState == EntityState.Attack)
        {
            RollState();
        }
    }

    public void RollState()
    {
        float roll = Random.value;
        for (int i = 0; i < stateChances.Length - 1; i++)
        {
            roll -= stateChances[i];
            if (roll <= 0)
            {
                SetState((EntityState)i);
                return;
            }
        }
        SetState((EntityState)(stateChances.Length - 1));
    }

    public void SetState(EntityState state)
    {
        entityState = state;
        switch (state)
        {
            case EntityState.Wander:
                float dist = Random.Range(minWandDist, maxWandDist);
                wandPos.x = Random.value - .5f;
                wandPos.y = Random.value - .5f;
                wandPos *= dist * 2;
                // while ((wandPos-(Vector2)PlayerControl.instance.transform.position).magnitude > playerAvoidRad )
                while (Mathf.Abs(wandPos.x) > xyMax.x || Mathf.Abs(wandPos.y) > xyMax.y)
                {
                    wandPos.x = Random.value - .5f;
                    wandPos.y = Random.value - .5f;
                    wandPos *= dist * 2;
                }
                break;
            case EntityState.Attack:
                fireTime = Random.Range(minFireTime, maxFireTime);
                if (swapWhileAttacking)
                {
                    RollState();
                }
                break;
            case EntityState.Pursuit:
                pursTime = Random.Range(minPursTime, maxPursTime);
                break;
            case EntityState.Idle:
                idleTime = Random.Range(minIdleTime, maxIdleTime);
                break;
        }
    }
}
