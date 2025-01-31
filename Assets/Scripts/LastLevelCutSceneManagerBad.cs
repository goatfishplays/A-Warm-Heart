using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelCutSceneManagerBad : MonoBehaviour
{
    public float time = 0f;
    public GameObject holder;
    public BodyManager playerBM;
    public Lerper playerLerper;
    public SpawnerManager[] guys;
    public Transform car;
    public float stopTime = 3f;
    public float fadeInSTime = 9f;
    public float fadeInTime = 3f;
    public float fadeOutSTime = 13f;
    public float fadeOutTime = 3f;
    public float atkSTime = 6f;
    public float atkTime = 3f;
    public SpriteRenderer black;
    public GameObject thx;
    public GameObject playAgain;

    // Start is called before the first frame update
    void Start()
    {
        if (Healthbar.instance)
        {

            Healthbar.instance.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > fadeOutSTime + fadeOutTime + 0.5f)
        {
            playAgain.SetActive(true);
        }
        else if (time > fadeOutSTime)
        {
            thx.SetActive(true);
            // playerRefBM.gameObject.SetActive(true); 
            playerLerper.enabled = false;
            Destroy(holder);
            playerBM.transform.position = Vector2.zero;
            // GenPlayerRef(); 
            Color temp = Color.black;
            temp.a = black.color.a - Time.deltaTime / fadeOutTime;
            black.color = temp;
        }
        else if (time > fadeInSTime)
        {
            foreach (SpawnerManager s in guys)
            {
                s.AimAll(car.transform.position);
                s.FireAll();
            }
            Color temp = Color.black;
            temp.a = black.color.a + Time.deltaTime / fadeInTime;
            black.color = temp;
        }
        else if (time > atkSTime)
        {
            foreach (SpawnerManager s in guys)
            {
                s.AimAll(car.transform.position);
                s.FireAll();
            }
        }
        else if (time > stopTime)
        {
            PlayerControl.instance.updating = false;
            playerBM.transform.position = car.position + new Vector3(0.5f, -0.5f);
            playerLerper.enabled = true;
        }

    }

    // public void GenPlayerRef()
    // {
    //     if (playerBM.heartChanged)
    //     {
    //         playerRefBM.gameObject.SetActive(false);
    //         return;
    //     }


    //     if (playerBM.handLChanged)
    //     {
    //         playerRefBM.handL.SetActive(false);
    //     }
    //     else
    //     {
    //         playerRefBM.handL.GetComponent<SpriteRenderer>().sprite = emptyHand;
    //     }

    //     if (playerBM.armLChanged)
    //     {
    //         playerRefBM.armL.SetActive(false);
    //     }
    //     if (playerBM.shoulderLChanged)
    //     {
    //         playerRefBM.shoulderL.SetActive(false);
    //     }


    //     if (playerBM.handRChanged)
    //     {
    //         playerRefBM.handR.SetActive(false);
    //     }
    //     else
    //     {
    //         playerRefBM.handR.GetComponent<SpriteRenderer>().sprite = emptyHand;
    //     }


    //     if (playerBM.armRChanged)
    //     {
    //         playerRefBM.armR.SetActive(false);
    //     }
    //     if (playerBM.shoulderRChanged)
    //     {
    //         playerRefBM.shoulderR.SetActive(false);
    //     }

    //     if (playerBM.eyeLChanged)
    //     {
    //         playerRefBM.eyeL.GetComponent<SpriteRenderer>().sprite = eyepatch;
    //     }
    //     if (playerBM.eyeRChanged)
    //     {
    //         playerRefBM.eyeR.GetComponent<SpriteRenderer>().sprite = eyepatch;
    //     }

    //     if (playerBM.legLChanged)
    //     {
    //         playerRefBM.legL.GetComponent<SpriteRenderer>().sprite = pegLeg;
    //         playerRefBM.footL.GetComponent<SpriteRenderer>().sprite = pegFoot;
    //     }
    //     else if (playerBM.footLChanged)
    //     {
    //         playerRefBM.footL.GetComponent<SpriteRenderer>().sprite = pegFoot;
    //     }
    //     if (playerBM.legRChanged)
    //     {
    //         playerRefBM.legR.GetComponent<SpriteRenderer>().sprite = pegLeg;
    //         playerRefBM.footR.GetComponent<SpriteRenderer>().sprite = pegFoot;
    //     }
    //     else if (playerBM.footRChanged)
    //     {
    //         playerRefBM.footR.GetComponent<SpriteRenderer>().sprite = pegFoot;
    //     }




    // }


}
