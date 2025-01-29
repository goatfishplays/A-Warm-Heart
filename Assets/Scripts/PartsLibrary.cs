using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLibrary : MonoBehaviour
{
    [System.Serializable]
    public struct Part
    {
        public SpawnerSO partSO;
        public string partPart;
    }
    public static PartsLibrary instance;
    public Part[] atkParts;
    public Part[] otherParts;


    // public SpawnerSO[] arms;
    // public SpawnerSO[] hands;
    // public SpawnerSO[] shoulders;
    // public SpawnerSO[] legs;
    // public SpawnerSO[] feet;

    // Start is called before the first frame update
    private void Awake()
    {

        instance = this;
    }
}
