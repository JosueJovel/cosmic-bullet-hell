using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSmoothed : MonoBehaviour
{
    // Start is called before the first frame update

    //GENERAL MOVEMENT
    public float moveSpeed = 5;  //a scalar that dictates how fast an object will move by default
    private float activeMoveSpeed;  
    public Rigidbody2D rb;   //this is the player's own rigidbody component being referenced here
    private Vector2 moveDirection;    //a vector that dictates the direction that player will be moving

    //SMOOTHDAMP
    private Vector2 smoothedDirection;  //Vector that will represent the intermediary vector between changing directions
    private Vector2 smoothedVelocity;   //Vector required for the Smoothdamp function to use as a reference for calculations
    public float rateOfDirectionChange = .01f;  //How fast to transition between vectors (in seconds)




    //DASH
    public float dashSpeed = 10;   //how much faster ship should move during a dash
    public float dashlength = 1f;  //how long the dash should last
    public float dashCooldown = .3f;  //how long to wait before being allowed to dash again
    private float dashCountdown;        //this variable will keep track of how much time is left for a dash to be active
    private float dashCooldownCountdown;   //this keeps track of how much time is left for the dash cooldown to end
   
   
    //SPRITE
    SpriteRenderer sprite;   //This is the player's own spirterendered component being referenced

    // Update is called once per frame

    private void Start() {
        activeMoveSpeed = moveSpeed;
        sprite = GetComponent<SpriteRenderer>();
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
        //POTENTIAL TODO: Consider adding a tiny delay to when the player object should stop moving whenever the direction is 0
        //This is because as it stands, movement feels stuttery because when pressing keys fast, keys on opposite sides will sometimes cancel each other out, causing slight stutters in movement.
        //OTHER SOLUTION: Somehow find a way to only consider the last two keys being pressed. This would fix the issue of accidentally holding two oppsite keys during swift maneuvers


        
    }

    void Dash() {
        if (Input.GetButton("Fire1")){
            if ((moveDirection != Vector2.zero) && dashCountdown <= 0 && dashCooldownCountdown <= 0) {    //A dash may only start if A. The player is holding a direction, B. There is no dash currently ongoing, and C. The cooldown after a dash has ended.
                activeMoveSpeed = dashSpeed;
                dashCountdown = dashlength;
                //TODO: Make the last part of the dash to be a somewhat gradual slowdown, so that decelartion is a gradient and not instant. This will effectively lengthing the dash, and therefore invulnerability, which is good.
                sprite.color = new Color (0, 0, 1, 1); //Changes player color while dashing, mostly used for testing reasons
            }
        }

        if (dashCountdown > 0){  //Keeps track of how long an active dash has left. Countsdown every frame, since it is called every frame, because this Dash() function is called every frame in Update


            dashCountdown -= Time.deltaTime;

            if (dashCountdown <= 0 || !Input.GetButton("Fire1")){        //Once the active part of the dash finally ends OR the player stops holding dash, reset the move speed back to normal speed, and start the dash cooldown (to prevent dash abuse)
                dashCountdown = 0;                                          //SELF NOTE FOR JOSUE: Dash cooldown mechanic entirely may be irrelevant if I opt to go for the dash resource option instead                  
                activeMoveSpeed = moveSpeed;                                //additionally, if the ship stops moving in a direction at any point, the dash prematurely ends and cooldown starts
                dashCooldownCountdown = dashCooldown;
                sprite.color = new Color (1, 1, 1, 1);                      //SELF NOTE FOR JOSUE: Consider adding a small delay between dash ending and invulnerability/speed ending, to account for human reaction times, and to be a little more forgiving in general
            }
        }

        if (dashCooldownCountdown > 0) {   //Keeps track/counts down of how long the dash cooldown has left. Also counts down every frame, same as above.
            dashCooldownCountdown -= Time.deltaTime;    
        }                                               //When the cooldown finally reaches 0, this if statement stops running, and dashing will be available to the player again.
    }
}
