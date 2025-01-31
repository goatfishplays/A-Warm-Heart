using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeloApplier : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 startVelo = Vector2.zero;
    public float timeNext = 1f;
    public Vector2 nextVelo = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetVelo(startVelo);
    }
    private void Update()
    {
        timeNext -= Time.deltaTime;
        if (timeNext < 0)
        {
            SetVelo(nextVelo);
        }
    }

    public void SetVelo(Vector2 velo)
    {
        rb.velocity = velo;
    }
}
