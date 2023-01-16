using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBlast : MonoBehaviour
{
    //This script is attached to the damaging laser that destroys the player ship. It is instantiated after the aiming laser prefab is instantiated which
    //telegraphs to the player that a beam blast is about to hit the area the aiming laser is in. Beam blast and damaging laser refer to the same thing.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This function destroys player on contact or collision. One of the objects must have a dynamic Rigidbody in order for this to work.
    //In this case the player has the dynamic Rigidbody. When BeamBlast has a dynamic Rigidbody it gets knocked away when it's collided
    //with because gravity works on dynamic rigidbodies.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

}
