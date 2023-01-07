using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;  //a scalar that dictates how fast an object will move
    public Rigidbody2D rb;   //this is the player's own rigidbody component being referenced here
    private Vector2 moveDirection;    //a vector that dictates the direction that player will be moving


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
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
        rb.MovePosition((Vector2)transform.position + (moveDirection * moveSpeed * Time.deltaTime));

        //The above is Identical to the transform.translate method from the original player script. The only difference is that it does this with a rigidbody objet, meaning
        //the object and interact with colliders, which is very important.

        //How the entire script works: The movedirection vector is being updated every frame. For example, if a player does not give any inputs, then the move direction is being re-calculated every frame and set to 0
        //The Move function is also being recalculated a few times every internal loop, using the ever changing move direction. 
        //If a player does start giving inputs, the movedirection vector now points to a direction. Then, the move function takes in that direction, and starts translating the player object in that direction
        //Movespeed is simply a scalar to increase/decrease the physical speed the object actually moves with.
        //Naturally, the moment input stops, moveDirection is recalculated back to 0 by the next frame. Then, the next time the move function is called, also around within the next frame, it will have a 0 move direction vector, and stop moving.
    }
}
