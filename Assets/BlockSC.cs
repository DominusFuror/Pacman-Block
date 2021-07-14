using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSC : MonoBehaviour
{
    public Color blockColor;

    public GameObject upBlock;
    public GameObject downBlock;
    public GameObject rightBlock;
    public GameObject leftBlock;
    void Start()
    {
        
    }

    public void SetColor (Color newColor)
    {
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {

            item.color = newColor;


        }
    }
    public void UpBlockActive(bool active)
    {

        upBlock.SetActive(active);


    }
    public void DownBlockActive(bool active)
    {

        downBlock.SetActive(active);


    }
    public void RightBlockActive(bool active)
    {

        rightBlock.SetActive(active);


    }
    public void LeftBlockActive(bool active)
    {

        leftBlock.SetActive(active);


    }
    void Update()
    {
        
    }
}
