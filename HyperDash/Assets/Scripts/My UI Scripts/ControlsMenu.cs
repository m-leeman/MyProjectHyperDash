using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlsMenu : MonoBehaviour
{
    /// <summary>
    /// This program provides the player two options for closing the controls menu.
    /// </summary>

    private void Update()
    {
        if (Input.GetKeyDown("escape")) //If the player press the escape key close controls menu.
        {
            gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);         //If the close button is pressed the controls menu is closed.
    }
}