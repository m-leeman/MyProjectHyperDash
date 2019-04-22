using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    /// <summary>
    /// This program is for the main menu buttons. 
    /// </summary>

    public Transform canvas;                      //The entire tutorial menu canvas.

    private void Start()
    {
        canvas.gameObject.SetActive(false);     //Deactivate when the game begins.
        Time.timeScale = 0.0f;  //As soon as the game starts it pauses all scenes.
    }

    public void Play () {
        SceneManager.LoadScene("Main Game");    /// When the play button is pressed, the Main Game will begin.
	}

    public void Quit()
    {
        Application.Quit();             /// When the quit button is pressed, the game application will finish.
        Debug.Log("Quited the game");   /// When in the Unity Editor, this logs to the console the app has been quited.
    }

    public void Tutorial()
    {
        canvas.gameObject.SetActive(true);      //Activate the control menu when the control button is pressed.
    }
}
