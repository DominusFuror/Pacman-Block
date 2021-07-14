using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseMessActivator : MonoBehaviour
{
 


    private void Start()
    {
        MainServerManager.SendMQTTMess(this.gameObject.name + "Start");
    }



}
