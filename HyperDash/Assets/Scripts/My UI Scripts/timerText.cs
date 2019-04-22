using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class timerText : MonoBehaviour
{
    /// <summary>
    /// This program updates the time text UI to show the current time during gameplay.
    /// </summary>

    private float startingTime;
    public Text timer;
    public static string currentTime;
    private readonly finalTime finalTime;

    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;   //Get the time when starting the game
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time - startingTime;      //How much time has passed from the beginning of the game.

        string minutes = ((int)time / 60).ToString("00");                   //Convert this time into a minute gran ularity. Convert it to a string into a "00" format.
        string seconds = ((int)time % 60).ToString("00");                   //Convert this time into a second granularity. Convert it to a string into a "00" format.
        string miliseconds = ((int)(time * 100f) % 100).ToString("00");     //Convert this time into a millisecond granularity. Convert it to a string into a "00" format.

        currentTime = minutes + ':' + seconds + ':' + miliseconds;          //Write in 00:00:00 format.
        timer.text = currentTime;                                           //Update the time text UI to show the current time.

        if (finalTime.Finished == true)
        {
            timer.gameObject.SetActive(false);
        }
    }
}
