using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// This script updates the last checkpoint the player has pasted.
    /// This script is attached to each of the checkpoints objects in the unity inspector.
    /// The current checkpoints location is used as the respawn point when the player dies.  
    /// </summary>

    public GameObject player;
    public static Vector2 currentCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)     // When an object enters the collider.
    {
        if (other.name == "Player")     // If the object is the player.
        {
            currentCheckpoint = gameObject.transform.position;  //Then the current checkpoint location is updated.
        }
    }
}
