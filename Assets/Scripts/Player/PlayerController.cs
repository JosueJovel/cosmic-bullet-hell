using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //GENERAL MOVEMENT
    public float moveSpeed = 5;  //a scalar that dictates how fast the player will move by default
    private float activeMoveSpeed;  //variable for player movement speed, because during gameplay the player's speed will change constantly
    private Vector2 moveDirection;    //a vector that dictates the direction that player will be moving

    //SMOOTHDAMP
    private Vector2 smoothedDirection;  //Vector that will represent the intermediary vector between changing directions
    private Vector2 smoothedVelocity;   //Vector required for the Smoothdamp function to use as a reference for calculations
    public float rateOfDirectionChange = .007f;  //How fast to transition between vectors (in seconds)
    




    //DASH
    public float dashSpeed = 10;   //how much faster ship should move during a dash
    public float dashlength = .1f;  //how long the dash should last
    public float dashCooldown = .3f;  //how long to wait before being allowed to dash again
    public float dashSlowdown = 1f;   //length of the dash's "Slowdown" phase
    public float slowdownScalar = 2.4f;
    private float dashCountdown;        //this variable will keep track of how much time is left for a dash to be active
    private float dashCooldownCountdown;   //this keeps track of how much time is left for the dash cooldown to end
    private float dashSlowdownCountdown;   //keeps track of time left for the slowdown phase
    private float slowdownRate;         //derived value dictating how much to slow down the player (per frame) during the slowdown part of the dash
   
   
    //COMPONENTS
    //Note: Sprite is attached sperately, as a child to an object with this script attached to it. The reason for that is to allow sprite manipulation while keeping hitboxes consistent
    PlayerHealth playerHealth;    //Creates a new variable to hold the player component/class/script, so that it's script can be referenced. 
    public Rigidbody2D rb;   //this is the player's own rigidbody component being referenced

    // Update is called once per frame

    private void Start() {
        activeMoveSpeed = moveSpeed;
        slowdownRate = ((dashSpeed - moveSpeed) * slowdownScalar);  //Higher scalar = velocity will decrease faster/sooner
        playerHealth = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        ProcessInputs();
        Dash();
        Move();

    }

    void ProcessInputs(){                                       //This is a function that, every time its called, takes in all directional inputs currently being sent
        float moveX = Input.GetAxisRaw("Horizontal");            //directional input cam be either 0 or 1. So if the up key is being held, unity will register 1 in the up direction.
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;              //Then, those inputs are interpreted into a direction/ a vector. For example, at one moment, up and left could be sent. This 
                                                                          //is then translated to a vector with magnitudes of 1 going up, and 1 going left, which means its a vector at angle 135 degrees.
                                                                          //normalized is a simple method that caps vector magnitued to one, no matter which direction it's pointing.
    }

    void Move() {


        smoothedDirection = Vector2.SmoothDamp(smoothedDirection, moveDirection, ref smoothedVelocity, rateOfDirectionChange);
        //What smoothdamp is doing: It takes in two vectors, the current direction, and the direction that the player has just inputted. Then, Smoothdamp will shift the current direction to
        //the inputted direction at the rate specified (The rate indicates how long the shift should take; 1f would indicate change it from current to new direction over one second)
        // Note: The reference smoothedVelocity is only there for calculation purposes, it is unused outside of this function.


        rb.velocity = smoothedDirection * activeMoveSpeed;
        //This is the velocity form of movement. It is extremely similiar to the rb.moveposition method of movement. As with regular vectors, it is calculated simply by multiplying direction and magnitude.


        
    }

    void Dash() {
        if (Input.GetButtonDown("Fire1")){
            if (dashCountdown <= 0 && dashCooldownCountdown <= 0) {    //A dash may only start if A. There is no dash currently ongoing, and B. The cooldown after a dash has ended.
                dashSlowdownCountdown = 0;  //interrupts slowdown if still in progress
                activeMoveSpeed = dashSpeed;
                dashCountdown = dashlength; 
                StartCoroutine(playerHealth.DashingIframes()); //Upon tapping dash, the coroutine in the player health script is triggered. That coroutine grants invulnerability for some time
            }
        }

        if (dashCountdown > 0){  //Keeps track of how long an active dash has left. Countsdown every frame, since it is called every frame, because this Dash() function is called every frame in Update


            dashCountdown -= Time.deltaTime;

            if (dashCountdown <= 0){                //Once the active part of the dash finally ends, gradually slow the player back to normal speed, and start the dash cooldown (to prevent dash abuse)
                dashSlowdownCountdown = dashSlowdown;               
                dashCooldownCountdown = dashCooldown;                
            }
        }

        if (dashSlowdownCountdown > 0) {
            dashSlowdownCountdown -= Time.deltaTime;
            activeMoveSpeed = Mathf.MoveTowards(activeMoveSpeed, moveSpeed, slowdownRate * Time.deltaTime); //The ship gradually slows down after the dash to give a feeling of smooth decelartion, instead of instant velocity changing.
            //NOTE: Slowdown dictates for how long the above MoveTowards will run. So if slowdown phase is really short, the movetowards will run for very little, leading to more sudden snap back to regular speed

            if (dashSlowdownCountdown <= 0) {      
                activeMoveSpeed = moveSpeed;                  
            }
        }

        if (dashCooldownCountdown > 0) {   //Keeps track/counts down of how long the dash cooldown has left. Also counts down every frame, same as above.
            dashCooldownCountdown -= Time.deltaTime;    
        }                                               //When the cooldown finally reaches 0, this if statement stops running, and dashing will be available to the player again.
    }
    public Vector2 getDirection() {
        return rb.velocity;
    }
}
