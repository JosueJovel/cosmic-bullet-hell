using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;  //a scalar that dictates how fast an object will move by default
    public Rigidbody2D rb;   //this is the player's own rigidbody component being referenced here
    private Vector2 moveDirection;    //a vector that dictates the direction that player will be moving
    private float activeMoveSpeed;   
    public float dashSpeed;   //how much faster ship should move during a dash
    public float dashlength = .5f;  //how long the dash should last
    public float dashCooldown = 1f;  //how long to wait before being allowed to dash again

    private float dashCountdown;        //this variable will keep track of how much time is left for a dash to be active
    private float dashCooldownCountdown;   //this keeps track of how much time is left for the dash cooldown to end
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

    }

    //FixedUpdate is called a set amount of times per loop, better for consistency, such as physics calculations.
    void FixedUpdate() {
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
        rb.MovePosition((Vector2)transform.position + (moveDirection * activeMoveSpeed * Time.deltaTime));  //SELF NOTE: Consider adding a tiny delay to when the player object should stop moving whenever the direction is 0
                                                                                                            //This is because as it stands, movement feels stuttery because when pressing keys fast, keys on opposite sides will sometimes cancel each other out, causing slight stutters in movement.
                                                                                                            //This may also simply be an issue inherent to transaltion type movement, where acceleration/momentum don't exist. Because of that, may still be worth it to consider that type of movement.

        //The above is Identical to the transform.translate method from the original player script. The only difference is that it does this with a rigidbody objet, meaning
        //the object and interact with colliders, which is very important.

        //How the entire script works: The movedirection vector is being updated every frame. For example, if a player does not give any inputs, then the move direction is being re-calculated every frame and set to 0
        //The Move function is also being recalculated a few times every internal loop, using the ever changing move direction. 
        //If a player does start giving inputs, the movedirection vector now points to a direction. Then, the move function takes in that direction, and starts translating the player object in that direction
        //Movespeed is simply a scalar to increase/decrease the physical speed the object actually moves with.
        //Naturally, the moment input stops, moveDirection is recalculated back to 0 by the next frame. Then, the next time the move function is called, also around within the next frame, it will have a 0 move direction vector, and stop moving.
    }

    void Dash() {
        if (Input.GetKeyDown(KeyCode.Space)){
            if ((moveDirection != Vector2.zero) && dashCountdown <= 0 && dashCooldownCountdown <= 0) {    //A dash may only start if A. The player is holding a direction, B. There is no dash currently ongoing, and C. The cooldown after a dash has ended.
                activeMoveSpeed = dashSpeed;
                dashCountdown = dashlength;
                //TODO: force the object to move right on its own, and stay going that direction until dash ends or direction overwritten.
                //Current issue: if a default direction is manually applied here, it will be set to 0 one frame later
                sprite.color = new Color (0, 0, 1, 1); //Changes player color while dashing, mostly used for testing reasons
            }
        }

        if (dashCountdown > 0){  //Keeps track of how long an active dash has left. Countsdown every frame, since it is called every frame, because this Dash() function is called every frame


            dashCountdown -= Time.deltaTime;

            if (dashCountdown <= 0 || !Input.GetKey(KeyCode.Space)){        //Once the active part of the dash finally ends OR the player stops holding dash, reset the move speed back to normal speed, and start the dash cooldown (to prevent dash abuse)
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
