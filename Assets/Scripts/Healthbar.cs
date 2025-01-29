using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public static Healthbar instance;
    public Entity entity;
    public Image fill;

    [Header("Alternate Heart")]
    public Color altColor;
    public Image heartIcon;
    public Sprite steelHeartSprite;
    public Sprite steelHeartSpriteBroken;

    public void SetFill(float percent)
    {
        if (percent < 0)
        {
            percent = 0;
        }
        fill.fillAmount = percent;
    }

    public void SetAlternateHeart()
    {
        heartIcon.sprite = steelHeartSprite;
    }

    public void SetBrokenHeart()
    {
        fill.color = altColor;
        heartIcon.sprite = steelHeartSpriteBroken;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetFill(1);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        SetFill(entity.health / entity.maxHealth);
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     SetAlternateHeart();
        // }
    }
}
