using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    /// <summary>
    /// This program pauses the game at the very beginning so 
    /// the animation can be played without interruption.
    /// </summary>

    private readonly PauseMenuButtons PauseMenuButtons;
    private static bool paused;
    public static bool starting;

    void Start()
     {
        StartCoroutine(Pause());        //At the start, call the pause function.
     }
     
    IEnumerator Pause()
    {
        starting = true;                                //The game is in the starting phase.
        Time.timeScale = 0.0f;                          //Set the game speed to zero so that the game stops/freezes.
        yield return new WaitForSecondsRealtime(3f);    //Wait for 3 real-time seconds so the 3,2,1 animation can play.
        starting = false;                               //Afterwards, the game is no longer in the starting phase
        Time.timeScale = 1.0f;                          //The game is returned to normal speed.
        yield return new WaitForSecondsRealtime(1f);    //Wait for one second so the GO! sprite is shown.
        gameObject.SetActive(false);                    //Hide the animation afterwards.
    }
}
