using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : Attack
{
    public GameObject explosionPrefab;

    public override void Break()
    {
        GameObject summoned = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Attack summonedAtk = summoned.GetComponent<Attack>();
        if (summonedAtk)
        {
            summonedAtk.ownerID = ownerID;
        }
        base.Break();
    }
}
