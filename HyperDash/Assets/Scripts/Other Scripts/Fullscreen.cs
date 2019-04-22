using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    //to keep the application in fullscreen

    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen; 
    }

}
