using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NOTE: This script is attached to whatever game object is desired to be the source of this pattern. 
public class SpreadFire : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10; //How many bullets to be fired
    [SerializeField] private float startAngle = 90f, endAngle = 270f;
    [SerializeField] private float fireRate = .5f;
    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, fireRate); //This will repeatedly trigger the fire function defined below.
        //The first parameter is how long to wait to set off the invoke, the second indicates how long between every subsequent invoke.
    }

 

    //This defines the pattern that the bullets will come out with
    private void Fire() { 
        float angleStep = (endAngle - startAngle) / bulletsAmount;  //Dictates how much space should be between each bullet to ensure even spacing
        float angle = startAngle;   //This is the starting angle for the first bullet

        for (int i = 0; i < bulletsAmount; i++)   //This for loop calculates each bullet's individual direction, to ensure a spread pattern
        {
            //These determine the coordinates for the endpoint of a bullet, on the unit circle, if it were fired in the current angle
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);    
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulmoveVector = new Vector3(bulDirX, bulDirY, 0f);   //Direction vector is then determined from coordinates
            Vector2 bulDir = (bulmoveVector - transform.position).normalized;  //2d Direction is then extracted and normalized

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();   //Grabs an available bullet from the pool via BulletPool's getter
                bul.transform.position = transform.position;  //Sets the bullet's position to this objects own position
                bul.transform.rotation = transform.rotation;  //sets teh bullet's rotation to the same degree as this object's rotation
                bul.SetActive(true);                            //Bullet is marked active
                bul.GetComponent<Bullet>().SetMoveDirection(bulDir);  //Bullet is fired with direction calculated above

            angle += angleStep;  //increments the next angle for the bullet to come out of.
        }
    }
}
