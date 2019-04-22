using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Positioning : MonoBehaviour
{
    /// <summary>
    /// This program finds the rank of the player during the race and outputs it to
    /// the Rank Text UI.
    /// </summary>

    public Transform PlayerPos; //Position of the player.
    public GameObject AI1;  //The agent objects.
    public GameObject AI2;
    public GameObject AI3;
    public GameObject AI4;
    public Text RankText;   //The Rank Text UI.

    private int rank = 1;   //As the player starts in first rank.

    private List<GameObject> BehindAI = new List<GameObject>(); //List of all the agents behind the player's position.
    private List<GameObject> AheadAI = new List<GameObject>();  //List of all the agents ahead of the player's position.
    private readonly finalTime finalTime;

    void Start()
    {
        BehindAI.Add(AI1);  //All agent's start behind the player.
        BehindAI.Add(AI2);
        BehindAI.Add(AI3);
        BehindAI.Add(AI4);
    }

    void Update()
    {
        for(int i = 0; i < BehindAI.Count ; i++)    //For each agent that's currently in the Behind list.
        {
            if(BehindAI[i].transform.position.x > PlayerPos.position.x) //If they are actually ahead of the player.
            {
                AheadAI.Add(BehindAI[i]);   //Add them to the Ahead list.
                BehindAI.RemoveAt(i);       //Remove them from the Behind list.
                rank++;                     //Increase the rank of the player.
            }
        }

        for(int i = 0; i < AheadAI.Count ; i++)     //For each agent that's currently in the Ahead list.
        {
            if(AheadAI[i].transform.position.x < PlayerPos.position.x)  //If they are actually behind of the player.
            {
                BehindAI.Add(AheadAI[i]);   //Add them to the Behind list.
                AheadAI.RemoveAt(i);        //Remove them from the Ahead list.
                rank--;                     //Decrease the rank of the player.
            }

        }

        string currentRank = "Rank: " +  rank;  //Write the new rank of the player to the Rank Text UI.
        RankText.text = currentRank;

        if (finalTime.Finished == true)
        {
            RankText.gameObject.SetActive(false);
        }

    }
}
