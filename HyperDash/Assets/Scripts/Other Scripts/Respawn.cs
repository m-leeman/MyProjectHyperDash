using System;
using UnityEngine;


public class Respawn : MonoBehaviour
{
    /// <summary>
    /// This program respawns the player to the nearest checkpoint when it falls down a pitfall.
    /// </summary>

    public float threshold;                     //Threshold set to -5 in the Unity Editor.
    private readonly Checkpoint Checkpoint; 
    private static Vector2 resPos;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)   //If the player falls beyond -5 units.
        {
            RespawnPlayer();                    //Then the player has died and must respawn.
        }
    }


    public void RespawnPlayer()
    {
        resPos = Checkpoint.currentCheckpoint;      //Check what the last checkpoint the player touched in the Checkpoint program.
        transform.position = resPos;                //Respawn player at this last checkpoint.

    }
}



