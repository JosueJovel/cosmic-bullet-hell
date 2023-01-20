using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : MonoBehaviour
{

    public GameObject BeamBlast; //this grabs the BeamBlast prefab to instantiate with
    public GameObject AimingLaser; //this grabs the AimingLaser prefab to instantiate with
    
    private GameObject InstantiatedLaser; //instantiated laser is stored here; need a reference to aiming laser object to check if it exists or not

    
    public Vector3 MuzzleLocation = new Vector3(0, -5.25f, 0); //location that AimingLaser and BeamBlast will appear from

    public float AimingLaserTimer = 2.0f; //how long the the aiming laser will appear for
    public float BeamBlastTimer = 1.0f; //how long the beam blast will appear for
    public float Interval = 5.0f; //interval between beam weapon firings
    private float Timer = 0f; //keep track of time passed for the other variables

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime; //simple timer that goes up in value over time normalized with the time between frames

        if (InstantiatedLaser != null) //if aiming laser exists -> count down aiming laser timer
        {
            AimingLaserTimer -= Time.deltaTime;
        }
        
        if (Timer >= Interval) //if timer is over interval time then execute code
        {
            Timer = 0f; //reset timer

            //instantiates aiming laser with (aiming laser prefab, beam weapon position - certain y value so aiming laser doesn't appear in the middle of the beam weapon, rotation identity)
            InstantiatedLaser = Instantiate(AimingLaser, transform.position + MuzzleLocation, Quaternion.identity);
            Destroy(InstantiatedLaser, AimingLaserTimer);
        }


        //once aiming laser has been on for however many seconds, then BeamBlast is instantiated. Set to <= 0.01 because sometimes AimingLaserTimer ends up
        //at a value of 0.000124 or some other random small number and doesn't execute properly
        if (AimingLaserTimer <= 0.01) 
        {
            AimingLaserTimer = 2.0f;
            //destroys and instantiates in same line; second parameter is how long the BeamBlast sticks around for
            Destroy(Instantiate(BeamBlast, transform.position + MuzzleLocation, Quaternion.identity), (int) BeamBlastTimer); 
        }

    }

}
