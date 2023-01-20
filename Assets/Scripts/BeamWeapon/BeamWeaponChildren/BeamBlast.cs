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

    //This function destroys player on contact or collision. Because this beam uses isTrigger for collisions, it will automatically not have collision.
    //This also removes the need for it to have a rigidbody of its own.

    
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject); //Destruction/Damage will be handled on the player's end
    }

}
