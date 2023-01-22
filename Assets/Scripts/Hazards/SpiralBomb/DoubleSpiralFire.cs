using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpiralFire : MonoBehaviour
{

    [SerializeField] private float angle = 0f;
    [SerializeField] private float fireRate = .1f;
    [SerializeField] private float angleStep = 10f;
    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, fireRate); //This will repeatedly trigger the fire function defined below.
        //The first parameter is how long to wait to set off the invoke, the second indicates how long between every subsequent invoke.
    }

 

    //This defines the pattern that the bullets will come out with
    private void Fire() { 
   
        
        for (int i = 0; i <= 1; i++)   //This for loop is used to fire each bullet that will be part of one of the spiral arms. In this case, it's a double armed spiral, so for loop triggers twice for every call
        {
            //These determine the coordinates for the endpoint of a bullet, on the unit circle, if it were fired in the current angle.
            //Because this for loop runs twice, two bullets are spawned: One in the desired angle, and another in the opposite direction.
            //If more spiral arms are desired, the for loop will iterate more times (one for each desired spiral arm), and the angle increment (180f here) will need to be adjusted accordingly
            float bulDirX = transform.position.x + Mathf.Sin((angle + 180f * i ) * Mathf.PI / 180f);   
            float bulDirY = transform.position.y + Mathf.Cos((angle + 180f * i ) * Mathf.PI / 180f);

            Vector3 bulmoveVector = new Vector3(bulDirX, bulDirY, 0f);   //Direction vector is then determined from coordinates
            Vector2 bulDir = (bulmoveVector - transform.position).normalized;  //2d Direction is then extracted and normalized

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();   //Grabs an available bullet from the pool via BulletPool's getter
                bul.transform.position = transform.position;  //Sets the bullet's position to this objects own position
                bul.transform.rotation = transform.rotation;  //sets teh bullet's rotation to the same degree as this object's rotation
                bul.SetActive(true);                            //Bullet is marked active
                bul.GetComponent<Bullet>().SetMoveDirection(bulDir);  //Bullet is fired with direction calculated above

            angle += angleStep;  //increments the next angle for the bullet to come out of.
        }

        if (angle >= 360f) {
            angle = 0f;
        }
    }
}
