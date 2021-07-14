using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class PlayZoneGenerator : MonoBehaviour
{

    public GameObject player;
    public GameObject coin;
    public GameObject wall;
    public GameObject enemy;
    public Color blocksColor;
   const  int ZoneX = 40;
   const int ZoneY = 16;
    public char[,] configStringMassive = new char[ZoneX+1, ZoneY+1];


    public static int FoodCounter = 0;

    void Awake()
    {

        Generator();
   
    }

    // Update is called once per frame
    void Update()
    {
       
    }               


    public void Generator()
    {

        ConfigZoneReader();

        for (int y = 0; y < ZoneY+1; y++)
        {
            for (int x = 0; x < ZoneX + 1; x++)
            {
                if (configStringMassive[x, y] == '#')
                {
                    var block = MakeWallBlock(x, y);
                    block.GetComponent<BlockSC>().SetColor(blocksColor);
                    try
                    {
                        if (configStringMassive[x + 1, y] == '#')
                        {
                            block.GetComponent<BlockSC>().RightBlockActive(false);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (configStringMassive[x - 1, y] == '#')
                        {
                            block.GetComponent<BlockSC>().LeftBlockActive(false);
                        }

                    }
                    catch (Exception)
                    {


                    }
                    try
                    {
                        if (configStringMassive[x, y + 1] == '#')
                        {
                            block.GetComponent<BlockSC>().UpBlockActive(false);
                        }
                    }
                    catch (Exception)
                    {


                    }
                    try
                    {
                        if (configStringMassive[x, y - 1] == '#')
                        {
                            block.GetComponent<BlockSC>().DownBlockActive(false);
                        }
                    }
                    catch (Exception)
                    {


                    }
                    



                }
                if (configStringMassive[x, y] == 'P')
                {
                    Instantiate(player, new Vector3(x, y, 0), this.transform.rotation, this.transform);
                }
                if (configStringMassive[x, y] == '0')
                {
                    Instantiate(coin, new Vector3(x, y, 0), this.transform.rotation,this.transform);
                    FoodCounter++;
                }
                if (configStringMassive[x, y] == 'E')
                {
                    Instantiate(enemy, new Vector3(x, y, 0), this.transform.rotation, this.transform);
                 
                }

            }
                

         }
  }

        
        


    public void ConfigZoneReader()
    {
        StreamReader stream = new StreamReader("PacmanZoneConfig.txt");

        var configText = stream.ReadToEnd();

        int counter = 0;
        for (int y = 0; y < ZoneY + 1; y++)
        {
            counter = configText.LastIndexOf('\n');
            if (counter == -1)
            {
                counter = 0;
                for (int x = 0; x < ZoneX + 1; x++)
                {


                    configStringMassive[x, y] = configText[counter];
                    counter++;

                }
                break;
            }
            counter++;
            for (int x = 0; x < ZoneX + 1; x++)
            {


                configStringMassive[x, y] = configText[counter];
                counter++;

            }
            int i = configText.LastIndexOf('\n');
            configText = configText.Remove(i, configText.Length - i);

        }

    }
   
    public GameObject MakeWallBlock(int x, int y)
    {
        return Instantiate(wall, new Vector3(x, y, 0), this.transform.rotation,this.transform);


    }


}
