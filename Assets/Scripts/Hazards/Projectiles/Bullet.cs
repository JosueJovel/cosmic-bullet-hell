using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    public Rigidbody2D rb;   //Ensure to attach the rigidbody of whatever object this script is attached to.

    // Start is called before the first frame update
    private void OnEnable() {   //Whenever a bullet is "enabled", ie it is actived, this function will be called
        Invoke("Destroy", 3f);    //By using Invoke, The destroy function defined below is used on the bullet, within the time given by the second paramater. This effectively gives the bullet a lifespan
    }
    
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.deltaTime)); //How to make the bullet move. 
        //TODO: Give bullet prefab rigidbody2d, and collider2d with is trigger. Also make movement rigidbody moveeposition. (Also add an if statement, where if it touches literally anything at all, it is destroyed). 
    }

    public void SetMoveDirection(Vector2 dir) {    //This is a setter for direction, which other scripts will access when firing bullets in different directions
        moveDirection = dir;
    }

    private void Destroy(){
        gameObject.SetActive(false);   //To "destroy" a bullet, it's state is set to inactive.
    }

    private void OnDisable(){  //This simply ensures that no conflicts will arise. For example, if a bullet is somehow turned inactive, but its invoked destroy function is still ticking down, it is cancelled
        CancelInvoke();
    }

    /*private void OnTriggerEnter2D(Collider2D other) {   //If it touches anything not an "enemy" (lasers, other bullets), destory itself. Still undecided if want projectiles to be destroyed on contact
        if (!(other.tag == "Enemy"))
        {
          Invoke("Destroy", 0.017f);  
          //Destroy(); call a subroutine that adds a tiny delay before destruction, literally just a few frames
        }
    }*/
}
