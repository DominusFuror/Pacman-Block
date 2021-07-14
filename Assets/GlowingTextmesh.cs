using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingTextmesh : MonoBehaviour
{
    public float glowignspeed = 0.5f;
    public float minAlphaLevel = 170;
    TextMesh spriteRenderer;
    int step = 1;
    private void Start()
    {
        spriteRenderer = GetComponent<TextMesh>();
    
    }
    void Update()
    {
        Color currentColor= spriteRenderer.color;

        if (currentColor.a * 255f >= 255)
        {
            step *= -1;

        }
        if (currentColor.a * 255f <= minAlphaLevel)
        {
            step *= -1;

        }

        currentColor.a += step / 255f * glowignspeed;
        spriteRenderer.color = currentColor;



    }
}
