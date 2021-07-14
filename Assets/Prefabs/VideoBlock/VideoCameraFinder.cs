using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCameraFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<VideoPlayer>().targetCamera = Camera.main; 
    }

    
}
