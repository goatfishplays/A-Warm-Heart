using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public ItemSO itemSO;
    public int count = 1;
    public float pickupRad = 1f;

    private void FixedUpdate()
    {
        if ((PlayerControl.instance.transform.position - transform.position).magnitude < pickupRad)
        {
            PlayerControl.instance.GetComponent<Inventory>().AddItem(itemSO, count);
            Destroy(gameObject);
        }
    }
}
