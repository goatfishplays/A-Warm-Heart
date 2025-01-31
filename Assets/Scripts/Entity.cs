using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{

    [Header("Entity")]
    public int id = 0;
    public bool destroyOnDie = true;

    [Header("Health")]
    public float trueImmunity = 0f;
    public bool changingHealth = true;
    public float health = 100f;
    public float maxHealth = 100f;
    public float healthChangeRate = 1f;
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

    [Header("Body parts")]
    public float wheelSpeedModifier;
    public GameObject shield;
    public int maxShield = 0;
    public int curShield = 0;
    public float maxShieldTime = 20f;
    public float curShieldTime;
    public GameObject heartBreak;
    public bool steelHeartUnlocked = false;
    public bool canRevive = true;

    [Header("Dashes")]
    public int maxDashes = 0;
    public int dashes = 0;
    public float maxDashCD = 3;
    public float curDashCD;
    public float dashSpeed = 15f;

    public float dashImmunity = .1f;






    // Start is called before the first frame update 
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        trueImmunity -= Time.deltaTime;
        iFrameTime -= Time.deltaTime;
        if (curShield < maxShield)
        {
            curShieldTime += Time.deltaTime;
            if (curShieldTime >= maxShieldTime)
            {
                curShieldTime = 0;
                curShield++;
                shield.SetActive(true);
            }
        }
        if (dashes < maxDashes)
        {
            curDashCD += Time.deltaTime;
            if (curDashCD >= maxDashCD)
            {
                curDashCD = 0;
                dashes++;
            }
        }
        if (iFrameTime < 0)
        {
            iFrameTime = -1;
        }
        if (changingHealth)
        {
            ChangeHealth(healthChangeRate * Time.deltaTime * (steelHeartUnlocked ? 2 : 1), false);
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

    public virtual void ChangeHealth(float delta, bool addsIframes = true, bool ignoresIframes = false)
    {
        if (changingHealth && trueImmunity <= 0)
        {
            if (delta < 0 && curShield > 0)
            {
                curShield -= 1;
                if (curShield == 0)
                {
                    shield.SetActive(false);
                }
                return;
            }
            if (delta > 0 || iFrameTime < 0.01f || ignoresIframes)
            {
                if (addsIframes)
                {
                    iFrameTime = iFrameAddTime;
                }
                health += delta;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                else if (health < 0)
                {
                    if (steelHeartUnlocked && canRevive)
                    {
                        health = maxHealth;
                        canRevive = false;
                        Instantiate(heartBreak, transform);
                        if (GetComponent<PlayerControl>())
                        {

                            Healthbar.instance.SetBrokenHeart();
                        }
                        return;
                    }
                    Die();
                }
            }
        }
    }

    public virtual void Move(Vector2 movementDir)
    {
        // cacl moveSpeedMult
        moveSpeedMult = 1 + wheelSpeedModifier;


        // calculate dir want to move and desired velo
        Vector2 targetSpeed = movementDir.normalized * (moveSpeed * moveSpeedMult);
        // change accell depending on situation(if our target target speed wants to not be 0 use decell)
        // need to split up so don't accidentally use accel for the axis that is supposed to deccel
        Vector2 accelRate = new Vector2(Mathf.Abs(targetSpeed.x) > .01f ? accel : deccel, Mathf.Abs(targetSpeed.y) > .01f ? accel : deccel);
        // calc diff between current and target 
        Vector2 speedDif = targetSpeed - rb.velocity;
        // applies accel to speed diff, raises to power so accel will increase with higher speeds then applies to desired dir
        Vector2 movement = new Vector2(Mathf.Sign(speedDif.x) * Mathf.Pow(Mathf.Abs(speedDif.x * accelRate.x), velPower), Mathf.Sign(speedDif.y) * Mathf.Pow(Mathf.Abs(speedDif.y * accelRate.y), velPower));
        // apply force
        // rb.AddForce(movement * Time.deltaTime);
        rb.AddForce(movement);

    }

    public virtual void Dash(Vector2 movementDir)
    {
        if (dashes > 0)
        {
            rb.AddForce(movementDir.normalized * dashSpeed, ForceMode2D.Impulse);
            dashes--;
            trueImmunity += 0.25f;
        }
    }

    public virtual void ApplyKnockback(Vector2 kb)
    {
        rb.AddForce(kb, ForceMode2D.Impulse);
    }

    public virtual void Die()
    {
        if (GetComponent<DropManager>() != null)
        {
            GetComponent<DropManager>().Die();
        }
        if (destroyOnDie)
        {
            Destroy(gameObject);
        }
        // Destroy(gameObject);
        if (GetComponent<PlayerControl>())
        {
            SceneSwitcher.loadScene("MainMenu");
        }

    }
}
