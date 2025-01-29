using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Collider2D hitbox;
    public Rigidbody2D rb;
    public AttackSO attackBase;
    public float liveTime = 0;
    public int ownerID = -1;

    // public LayerMask targetLayer;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        liveTime = attackBase.liveTime;
        hitbox = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
        {
            Break();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("World") && attackBase.breaksOnWall)
        {
            Break();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Entity"))
        {
            Entity otherEntity = other.gameObject.GetComponent<Entity>();
            // if (owner != null && other.gameObject.layer == owner.gameObject.layer && ownerImmune)// don't hit owner/other teammates
            if ((ownerID == otherEntity.id && attackBase.ownerImmune) || (!attackBase.ignoresIframes && otherEntity.iFrameTime > 0))// don't hit owner/other teammates
            {
                return;
            }

            // otherEntity.ApplyKnockback(rb.velocity.normalized * kb);
            otherEntity.ApplyKnockback((other.transform.position - transform.position).normalized * attackBase.kb);
            // otherEntity.ApplyKnockback(other. * kb);
            otherEntity.ChangeHealth(-attackBase.damage, attackBase.addsIframes, attackBase.ignoresIframes);
            if (attackBase.breaksOnHit)
            {
                Break();
            }
        }
    }

    // public virtual void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("World") && attackBase.breaksOnWall)
    //     {
    //         Break();
    //     }
    //     else if (other.gameObject.layer == LayerMask.NameToLayer("Entity"))
    //     {
    //         Entity otherEntity = other.gameObject.GetComponent<Entity>();
    //         // if (owner != null && other.gameObject.layer == owner.gameObject.layer && ownerImmune)// don't hit owner/other teammates
    //         if ((ownerID == otherEntity.id && attackBase.ownerImmune) || (!attackBase.ignoresIframes && otherEntity.iFrameTime > 0))// don't hit owner/other teammates
    //         {
    //             return;
    //         }

    //         // otherEntity.ApplyKnockback(rb.velocity.normalized * kb);
    //         otherEntity.ApplyKnockback((other.transform.position - transform.position).normalized * attackBase.kb);
    //         // otherEntity.ApplyKnockback(other. * kb);
    //         otherEntity.ChangeHealth(-attackBase.damage, attackBase.addsIframes, attackBase.ignoresIframes);
    //         if (attackBase.breaksOnHit)
    //         {
    //             Break();
    //         }
    //     }
    // }

    public virtual void Break()
    {
        Destroy(gameObject);
    }
}
