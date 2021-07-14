using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBlocj : MonoBehaviour
{



    void Start()
    {
        
    }

    public void GameStartButtonPressed()
    {

        GameObject.FindObjectOfType<MainServerManager>().Phase = "PackManPhase";

    }
}
