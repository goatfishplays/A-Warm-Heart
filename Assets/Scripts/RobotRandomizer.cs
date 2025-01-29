using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRandomizer : MonoBehaviour
{
    public BodyManager bm;
    public Vector2Int numAtkPartsRange;
    public Vector2Int numOtherPartsRange;

    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BodyManager>();
        int numAtkParts = Random.Range(numAtkPartsRange.x, numAtkPartsRange.y);
        for (int i = 0; i < numAtkParts; i++)
        {
            int partInd = Random.Range(0, PartsLibrary.instance.atkParts.Length);
            bm.SetRandom(PartsLibrary.instance.atkParts[partInd].partPart, PartsLibrary.instance.atkParts[partInd].partSO);
        }

        int numOhterParts = Random.Range(numOtherPartsRange.x, numOtherPartsRange.y);
        for (int i = 0; i < numOhterParts; i++)
        {
            int partInd = Random.Range(0, PartsLibrary.instance.otherParts.Length);
            bm.SetRandom(PartsLibrary.instance.otherParts[partInd].partPart, PartsLibrary.instance.otherParts[partInd].partSO);
        }


        Destroy(this);
    }

}
