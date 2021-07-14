using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingSprite : MonoBehaviour
{
    public float glowignspeed = 0.5f;
    public float minAlphaLevel = 140;
    Image spriteRenderer;
    int step = 1;
    private void Start()
    {
        spriteRenderer = GetComponent<Image>();

    }
    public Image fon;
    void Update()
    {
       Color currentColor= spriteRenderer.color;

        if (currentColor.a * 255 >= 254)
        {
            step = -1;
            fon.color = new Color(0, 0, 0, 1);

        }
        if (currentColor.a * 255 <= minAlphaLevel)
        {
            step = 1;

        }

        currentColor.a += step / 255f * glowignspeed;
        spriteRenderer.color = currentColor;



    }
}
