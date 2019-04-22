using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MyAcademy : Academy
{
    /// <summary>
    /// This script is the academy for the curriculum learning. 
    /// </summary>

    public GameObject FinishingFlagPos;

    [Header("Specific to HyperDash Curriculum Learning")]   
    public float xcoordinate = 3f;  
    public float ycoordinate = 0f;  // The initial position of the flag.

    public override void InitializeAcademy()
    {
    }

    // AcademyReset is called after each episode.
    public override void AcademyReset()
    {
        xcoordinate = resetParameters["xcoordinate"]; // The coordinates are reset to the values given in the JSON curriculum learning script.
        ycoordinate = resetParameters["ycoordinate"];
    }

    // AcademyStep is called per step of the stimulation.
    public override void AcademyStep()
    {
       FinishingFlagPos.transform.position = new Vector2(xcoordinate, ycoordinate); // the flag positioned at the cooridnates for the curriculum lesson. 
    }
}
