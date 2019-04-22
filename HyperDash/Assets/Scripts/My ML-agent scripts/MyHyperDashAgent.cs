using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MyHyperDashAgent : Agent
{
    /// <summary>
    /// This script defines the observation space, the action space and the reward system of the agent.
    /// It also includes the animation of the agent.
    /// </summary>

    [SerializeField] private LayerMask m_WhatIsGround;  //For the AI to know what the ground is (i.e. the platform blocks).

    public GameObject Finish;
    Vector2 startPos;
    private Rigidbody2D rb;
    private Transform GroundCheck;  //The location of the ground checking object. It's attached to the agent as a child.
    const float k_GroundedRadius = .2f;
    public float maxValueX;     // The maximum value of the X coordinate.
    public float maxValueY;     // The maximum value of the Y cooridnate.

    private RayPerception rayPer;
    public bool useVectorObs;
    private Animator anim;
    private bool facingRight = true;
    private bool Grounded = false;
    private bool jump = false;


    private void Awake()
    {
        // Setting up references at the beginning.
        GroundCheck = transform.Find("GroundCheck"); //Finding location of GroundCheck location.

    }

    public override void InitializeAgent()  /// Initializing the agent by its starting location, rigidbody, animation
    {                                       /// and the attached RayPerception script.
        base.InitializeAgent();

        startPos = gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rayPer = GetComponent<RayPerception>();
        anim = GetComponent<Animator>();

    }

    private void FixedUpdate()  
    {
        Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, m_WhatIsGround);    // What objects the agent is standing on.

        for (int i = 0; i < colliders.Length; i++)  //For each object the agent is standing on.
        {
            if (colliders[i].gameObject != gameObject) //If the object is not the agent itself.
                Grounded = true;    //Then the agent is standing on the ground.
        }

        anim.SetBool("Ground", Grounded);   //Play a Grounded animation if grounded.

        // Play the vertical falling/jumping animation if the agent is moving vertically. If y velocity is positive, play jump animation. If negative, then play falling. 
        anim.SetFloat("vSpeed", rb.velocity.y);
    }



    public override void CollectObservations()  //Obseravtions of the agent.
    {

        if (useVectorObs)   // If the agent is using Vector Observations. This is ticked to TRUE in the Unity Inspector.
        {
        
        var rayDistance = 15f;  // The distance of each raycast.
        float[] rayAngles = { 0f, 5f, 10f, 22.5f, 45f, 67.5f, 90f, 112.5f, 135f, 157.5f, 180f, 202.5f, 225f, 247.5f, 270f, 292.5f, 315f, 337.5f, 350f, 355f }; // angles of each raycast. There's 20 raycasts.
        var detectableObjects = new[] { "Platform blocks", "Finish"};   // The different objects the raycasts need to identify.
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));   // Calling the Perceive method in the RayPerception script and using that as the agent's Vector Observation. 
        
        AddVectorObs(transform.position.x / maxValueX); // The current coordinates of the agent as the observation. These are discretized between a range of [-1,1].
        AddVectorObs(transform.position.y / maxValueY);
        }
         

    }


    public override void AgentAction(float[] vectorAction, string textAction)   // The action space of the agent.
    {
        float runAnim = 0f;

        Vector2 dirToGo = Vector2.zero; // A zero vector [0 ; 0]. Represents the direction the agent is moving. 
        int action = Mathf.FloorToInt(vectorAction[0]);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, m_WhatIsGround); // Checking what colliders overlap the GroundCheck object.

        switch (action)
        {
            case 0:             //Action 0 is the jumping action.
                jump = true;
                break;

            case 1:             //Action 1 is the right movement.
                dirToGo = Vector2.right * 1f;   // Direction vector rewritten to have positive x value [1 ; 0]. 

                runAnim = 1f;
                if (!facingRight)
                {
                    Flip(); //flip the agent sprite if it's facing left.
                }
                break;
            
            case 2:             //Action 2 is the left movement.
                dirToGo = Vector2.right * -1f;  // Direction vector rewritten to have negative x value [-1 ; 0]. 

                runAnim = 1f;
                if (facingRight)
                {
                    Flip(); //flip the agent sprite if it's facing right.
                }
                break;
        }

        anim.SetFloat("Speed", runAnim);    //The running animation is played when runAnim = 1f;
        rb.velocity = new Vector2(dirToGo.x * 12f, rb.velocity.y);  //Add velocity to the agent's rigidbody in defined direction vector.

        if (Grounded && jump && anim.GetBool("Ground"))     //If the player is grounded and jumping and is playing a ground animation.
        {
            // Make the agent jump by adding a vertical impulse force and setting the ground booleans false.
            Grounded = false;
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);

        }

        jump = false;


        //Reward system of the agent:

        AddReward(-0.0002f);    //Punished for each step it takes.


        if (gameObject.transform.position.y < -1.0f)    // If the agent is falling down a pitfall.
        {
            Done();             //End the training episode. 
            AddReward(-1f);     //Add a negative reward.
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)  //If the agent touches the collider of the finish 
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            AddReward(1f);  //Then give a positive reward.
            Done();         //Episode is finished.
        }
    }


    private void Flip()     //Method for flipping the agent's sprite when it's facing the wrong way.
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;   //Multiplying by -1 is like a mirror effect.
        transform.localScale = theScale;
    }



    public override void AgentReset()
        {
            gameObject.transform.position = startPos;   //When the episode is finished then reset the agent back to the initial position.
        }

}


