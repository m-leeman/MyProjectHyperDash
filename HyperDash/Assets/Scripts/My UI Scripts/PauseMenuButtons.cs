using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    /// <summary>
    /// This program is for allowing the user to pause the game by pressing the escape key. When the player pauses the game,
    /// a UI pause menu appears with a selection of options including; resume game, restart the game and return to the main menu.
    /// Note, that the pause menu is not allowed to be actived during the starting phase. Otherwise, unpausing can cause the 
    /// starting phase to become shorter and cuts the 3,2,1 animation.
    /// </summary>

    public Transform canvas;                      //The entire pause menu canvas.
    public static bool PauseActive;               //This boolean indicates when the pause menu is activated.
    private readonly finalTime finalTime;
    private static bool Finished;                 //The Finished variable from the finalTime program. To know when the game has finished.
    private readonly Countdown Countdown;
    private static bool starting;                 //The starting variable from the Countdown program. To know when the game is starting.


    private void Start()
    {
        canvas.gameObject.SetActive(false);         //At the beginning disable the pause menu so it is not seen.
        PauseActive = false;
    }


    private void Update()
    {
        Finished = finalTime.Finished;      //Import these variables at each update
        starting = Countdown.starting;

        if (Input.GetKeyDown("escape") && Finished == false && starting == false)   //If the player press the escape key and it's not the beginning of the game or the end.
        {
            if (PauseActive == true)                    //If the pause menu was already opened.
            {
                canvas.gameObject.SetActive(false);     //Then, hide the pause menu.
                Time.timeScale = 1.0f;                  //Return the game to normal speed.
                PauseActive = false;                    //The pause menu is no longer active.
            }

            else                                        //  If the pause menu wasn't already opened.
            {
                canvas.gameObject.SetActive(true);      //Then, open the pause menu.
                Time.timeScale = 0.0f;                  //The game is frozen/stopped.
                PauseActive = true;                     //The pause has been activated.
            }
        }

    }

    public void Resume()        //Logic for the Resume button.
    {
        canvas.gameObject.SetActive(false);     //Pressing the resume button is the same as pressing the 
        Time.timeScale = 1.0f;                  //escape key when the pause menu is opened.
        PauseActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main Game");        //If the Restart button is pressed then reload the Main Game scene.
    }

    public void MainMenu()
    {
       SceneManager.LoadScene("Main Menu");         //If the Main Menu button is pressed then load the Main Menu scene.
    }

}
