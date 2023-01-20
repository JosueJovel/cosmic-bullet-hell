using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector2 moveDirection;
    public float parallaxSpeed = 1f;
    PlayerController playerController;    //Creates a new variable to hold the player component/class/script, so that it's script can be referenced. 
    [SerializeField] GameObject Player;   //This is here so that the object in question that is being referenced can be assigned to this script (The actual player game object in the scene). The actual attaching of the object to this variable is made manually in unity's inspector
    private Vector3 oldPosition;
    private Vector3 heading;


    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();  //From the object that was linked to this scipt, the player controller is extracted and copied here.
        oldPosition = Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        heading = Player.transform.position - oldPosition;
        //Make a call to Player movement's getDirection(); Note, neither direction or velocity work because technically, the ship never stops moving. It is simply being snapped abck to the playfield
        //SOLUTION: Need to 1. Tell if player is still or not, and 2. Get NET DIRECTION only, not input direction.
        //DETAILS: Every update, get the controller's transform.position. Compare to old position, Subtract the x and y values, this should get net direction. Then use that direction in move
        //heading.normalized
        //CURRENT ISSUE: backgroudn is moving based on the position difference relative to where the player spawns, for whatever reason.
        

        moveDirection = playerController.getDirection(); //May be unused if using heading below
        Move();
        oldPosition = Player.transform.position;
    }

    void Move() {
        transform.Translate(heading.normalized * parallaxSpeed * Time.deltaTime);   //Change so that it constantly uses player direction

    }

    
}
