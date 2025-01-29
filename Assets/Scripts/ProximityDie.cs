using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDie : MonoBehaviour
{
    public EnemyControl ec;
    public Entity entity;
    public float dieRad = 1f;
    public bool dying = false;
    public float idleTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EnemyControl>();
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dying)
        {
            idleTime -= Time.deltaTime;
            if (idleTime <= 0f)
            {
                entity.Die();
            }
        }
        else if ((transform.position - PlayerControl.instance.transform.position).magnitude < dieRad)
        {
            dying = true;
            ec.idleTime = idleTime + 1f;
            ec.entityState = EnemyControl.EntityState.Idle;

        }
    }
}
