using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashUser : MonoBehaviour
{
    public EnemyControl ec;
    public Entity entity;

    public Vector2 tryTicksRange = new Vector2(5, 100);
    public float tryTicks = 0;

    private void Start()
    {
        ec = GetComponent<EnemyControl>();
        entity = GetComponent<Entity>();
        tryTicks = Random.Range(tryTicksRange.x, tryTicksRange.y);
    }
    private void FixedUpdate()
    {
        if (tryTicks > 0)
        {

            tryTicks--;
        }
        else if (entity.dashes > 0)
        {
            tryTicks = Random.Range(tryTicksRange.x, tryTicksRange.y);
            ec.Dash();
        }
    }
}
