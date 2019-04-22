using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class finalTime : MonoBehaviour
{
    /// <summary>
    /// This program checks when the player has finished the game by touching 
    /// the flag. The program will then active the finishing menu and 
    /// show the players their final position and final time.
    /// </summary>

    private static string currentTime;
    private readonly timerText timerText;
    public Text position;
    public Text finishingTime;
    private string finishingSentence;
    public Transform canvas;
    private int counter;        //This variable will count the amount players that touches the finishing flag.
    public static bool Finished;    //Boolean to represent when the player has finished the game.

    private void Start()
    {
        canvas.gameObject.SetActive(false);     //Hide the finishing menu.
        Finished = false;
        counter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)     //When another object collides with the flag's collider.
    {
        if (other.name == "Player")     //If the other colliding object is the player.
        {
            Finished = true;    //Then the player has finished the game
            counter++;          // The position is incremented
            canvas.gameObject.SetActive(true);  //Activate the finishing menu.
            PositionAndTime();          //Call this function to get the final position and time.
            Time.timeScale = 0.0f;      //Freeze the game so the player can't move.
        }

        if (other.name != "Player")     //If the other colliding object is one of the AI players.
        {
            other.gameObject.SetActive(false);  //Deactivate the AI player as it is no longer needed.
            counter++;                  //Increase the rank.
        }
    }

    private void PositionAndTime()  
    {
        string positionNumber = "Final Position: " + ((int)counter).ToString();     //Convert the final rank integer and write a string.
        position.text = positionNumber;     //Update the final position text to show the final rank.

        currentTime = timerText.currentTime;        //Get the final time from the timerText script.
        finishingSentence = "Finishing time is " + currentTime;     
        finishingTime.text = finishingSentence;     //Update the finishing time text to show the final time.
    }

}
