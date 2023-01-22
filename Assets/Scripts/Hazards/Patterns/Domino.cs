using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] private GameObject laserSpawn;    //Holds Prefab of object that spawns lasers (Linked in unity inspector)
    public int laserAmount = 10;    //How many lasers this script should spawn
    public float interval = .1f;   //How long to wait between instantiating lasers (lower number = faster placement)
    public float waveDelay = 1f; //How long to pause between waves
    private Vector3 startPoint;  //starting point of first laser
    private Vector3 endPoint;    //ending point of last laser
    private Vector3 increment;   //used to calculate how much space to put between the lasers to guarantee even spacing

    

    // Start is called before the first frame update
    void Start()
    {
        //Grabs a point in the camera view, in this case the corners, and stores them as the world point equivalent
        startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        endPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        //Explaining the above: Think of the camera like x y coordinate plane, where the bottom left corner is the origin. a positive x = 1 is the very right edge, and a positive y = 1 is the top edge. It's all relative to the camera's "origin" at the bottom left.
        //Once the desired point, relative to the camera, is found, the point is converted to it's game plane equivalent. This is important because this plane vector is what's used by other objects



        Vector3 distance = endPoint - startPoint; //finds the vector that points from the start to the end 
        
        increment = distance / laserAmount;//divide the above vector by the laser amount; this dictates the space between lasers

       StartCoroutine(BackwardDomino());

       //TODO: Need a better management system that can be more easily manipulated from unity. 
       //Ideas: 1.Seperate this script into  a single domino routine, and then have a different script that triggers it multiple times(For a solution like this, will need a specific script for each domino direction/pattern). 
       //2.add public variables to represent from which points on the Camera, public variable to adjust orientation. This method would involve creating a bunch of objects out of sight to attach each custome domino script to.
       //First idea seems more appealing, as the "manager" script can more easily be made to constantly repeat for an indefinite amount of time.

    }

    IEnumerator ForwardDomino(){

        Vector3 currentDistance = startPoint;  //Grabs the location of the first laser to be spawned
       
        for (int i = 0; i < laserAmount; i++)     //Iterates once for every laser desired; Should place the desired amount of lasers, evenly spaced along the desired distance
        {
            Instantiate(laserSpawn, currentDistance, Quaternion.identity);   //Creates prefab at desired location, and with desired angle/rotation
            currentDistance += increment;                         //incremented, for calculating the next location of the laser down the line
            yield return new WaitForSeconds(interval);      //wait for a brief moment between each laser instantiaion to create a domino/wave effect
        }


    }

    IEnumerator BackwardDomino(){   //This does exactly the same as above, but with endpoint as current distance, and subtracting increment each run. Result is the laser wave moving backwards
        Vector3 currentDistance = endPoint;
        yield return new WaitForSeconds(1f);   //TESTING: Simple delay before the entire pattern fires off
        yield return ForwardDomino(); //calls for the first coroutine to run, and once it's done, this coroutine will move to the next statement
        yield return new WaitForSeconds(waveDelay);      //wait for a brief moment between each laser instantiaion to create a domino/wave effect
        
        for (int i = 0; i < laserAmount; i++)   
        {
            Instantiate(laserSpawn, currentDistance, Quaternion.identity);
            currentDistance -= increment;                       
            yield return new WaitForSeconds(interval); 
        }
    }  

    // Update is called once per frame
    void Update()
    {
        
    }
}
