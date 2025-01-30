using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [System.Serializable]
    public struct DropInfo
    {
        public GameObject gameObject;
        public float chanceToDrop;
        public Vector2Int dropCountRange;
    }

    public DropInfo[] drops;

    public void Die()
    {
        for (int j = 0; j < PlayerControl.instance.numDropRolls; j++)
            for (int i = 0; i < drops.Length; i++)
            {
                if (Random.value <= drops[i].chanceToDrop)
                {
                    GameObject drop = Instantiate(drops[i].gameObject, transform.position, Quaternion.identity, LevelManager.instance.dropHolder);

                    if (drop.GetComponent<Drop>() != null)
                    {
                        drop.GetComponent<Drop>().count = Random.Range(drops[i].dropCountRange.x, drops[i].dropCountRange.y);
                    }
                }
            }
    }
}
