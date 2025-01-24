using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLibrary : MonoBehaviour
{
    public static PartsLibrary instance;
    public SpawnerSO[] arms;
    public SpawnerSO[] hands;
    public SpawnerSO[] shoulders;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
}
