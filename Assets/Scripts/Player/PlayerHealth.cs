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

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay2D(Collider2D other) {   //Called when this object is involved in a collision with another collider that has a trigger
        if (other.tag == "Enemy" && damage_i == false)   //Checks if the collision was with an enemy, and invulnerability buffers are not currently applied
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

    }

    IEnumerator DamageIframes(){                //applies invulnerability for a set amount of time
        damage_i = true;
        yield return new WaitForSeconds(damageBuffer);
        damage_i = false;
    }
}
