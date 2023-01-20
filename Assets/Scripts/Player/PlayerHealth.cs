using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    private bool dashing_i = false; 
    private bool damage_i = false;
    public float damageBuffer = 2f;
    public float dashBuffer = .38f;
    PlayerController playerController;   //References the player object's player controller script

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay2D(Collider2D other) {   //Called when this object is involved in a collision with another collider that has a trigger
        if (other.tag == "Enemy" && damage_i == false && dashing_i == false)   //Checks if the collision was with an enemy, and invulnerability buffers (both while dashing and after taking damage) are not currently applied
        {
            TakeDamage();           //if so, take damage. Otherwise, nothing will happen (or different behavior may be implemented, with different tags)
            
        }
    }

    public void TakeDamage(){
        health -= 1;        //When called, health is first reduced by one
        if (health == 0)        
        {
            Destroy(gameObject);        //Upon reaching 0 health, game is over.
        }

        StartCoroutine(DamageIframes());              //If player is not destroyed, it gets a brief moment of invulnerability
        print("Current HP: " + health);             //TEMPORARY -- Later, a clear visual will be used to signify HP left
    }

    private IEnumerator DamageIframes(){                //applies invulnerability for a set amount of time after taking damage
        damage_i = true;
        yield return new WaitForSeconds(damageBuffer);
        damage_i = false;
        playerController.rb.AddForce(Vector2.zero);    
        //The purpose of the above line is to ensure that the collision of the player doesn't "go to sleep"; without this, a player could sit completely still
        //Inside a trigger collider, and because the ship is not moving, it won't cause another instance of ontriggerstay.
        
    }

    public IEnumerator DashingIframes(){                //applies invulnerability for a set amount of time after dashing
        dashing_i = true;
        yield return new WaitForSeconds(dashBuffer);
        dashing_i = false;
        playerController.rb.AddForce(Vector2.zero);
    }
}
