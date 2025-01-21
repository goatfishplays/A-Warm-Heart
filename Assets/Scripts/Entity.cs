using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Entity")]
    public int id = 0;

    [Header("Health")]
    public bool changingHealth = true;
    public float health = 100f;
    public float maxHealth = 100f;
    public float healthChangeRate = 0f;
    public float iFrameTime = 0f;
    public float iFrameAddTime = .2f;


    [Header("Movement Settings")]
    public bool canMove = true;
    public float moveSpeed = 15f;
    public float moveSpeedMult = 1f;
    public float accel = 1.2f;
    public float deccel = 3f;
    public float velPower = 1f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;




    // Start is called before the first frame update 
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        iFrameTime -= Time.deltaTime;
        if (iFrameTime < 0)
        {
            iFrameTime = -1;
        }
        if (changingHealth)
        {
            ChangeHealth(healthChangeRate * Time.deltaTime, false);
        }
    }

    protected virtual void FixedUpdate()
    {
    }

    public void SlowDown()
    {
        Vector2 speedDiff = -rb.velocity;
        Vector2 movement = Mathf.Pow(speedDiff.magnitude * deccel, velPower) * speedDiff.normalized;
        rb.AddForce(movement * Time.deltaTime);
    }

    public virtual void ChangeHealth(float delta, bool addsIframes = true)
    {
        if (changingHealth)
        {
            if (delta > 0 || iFrameTime < 0.01f)
            {
                health += delta;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                else if (health < 0)
                {
                    Die();
                }
                if (addsIframes)
                {
                    iFrameTime += iFrameAddTime;
                }
            }
        }
    }

    public virtual void Move(Vector2 movementDir)
    {
        // calculate dir want to move and desired velo
        Vector2 targetSpeed = movementDir.normalized * (moveSpeed * moveSpeedMult);
        // change accell depending on situation(if our target target speed wants to not be 0 use decell)
        // need to split up so don't accidentally use accel for the axis that is supposed to deccel
        Vector2 accelRate = new Vector2(targetSpeed.x > .01f ? accel : deccel, targetSpeed.y > .01f ? accel : deccel);
        // calc diff between current and target
        Vector2 speedDif = targetSpeed - rb.velocity;
        // applies accel to speed diff, raises to power so accel will increase with higher speeds then applies to desired dir
        Vector2 movement = new Vector2(Mathf.Sign(speedDif.x) * Mathf.Pow(Mathf.Abs(speedDif.x * accelRate.x), velPower), Mathf.Sign(speedDif.y) * Mathf.Pow(Mathf.Abs(speedDif.y * accelRate.y), velPower));
        // apply force
        // rb.AddForce(movement * Time.deltaTime);
        rb.AddForce(movement);

    }

    public virtual void ApplyKnockback(Vector2 kb)
    {
        rb.AddForce(kb, ForceMode2D.Impulse);
    }

    public virtual void Die()
    {

        // Destroy(gameObject);

    }
}
