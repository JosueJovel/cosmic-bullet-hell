using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : MonoBehaviour
{

    public GameObject BeamBlast; //this grabs the BeamBlast prefab to instantiate with
    public GameObject AimingLaser; //this grabs the AimingLaser prefab to instantiate with
    private GameObject InstantiatedLaser; //instantiated laser is stored here; need a reference to aiming laser object to check if it exists or not

    
    public Vector3 MuzzleLocation = new Vector3(0, -5.25f, 0); //location that AimingLaser and BeamBlast will appear from

    public float BeamBlastTimer = 1.0f; //how long the beam blast will appear for
    public float Interval = 1f; //interval between beam weapon firings

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireLaser());    //Firing the laser is handled via coroutine, because of the time delay component between telegraph and attack

    }

    IEnumerator FireLaser() 
    {
        InstantiatedLaser = Instantiate(AimingLaser, transform.position + MuzzleLocation, Quaternion.identity);   //Instantiates the aiming laser at the location of the attached object
        yield return new WaitForSeconds(Interval);              //Keeps aiming laser visible for (Interval) seconds
        Destroy(InstantiatedLaser);                             //Aiming laser is destroyed  
        BeamBlast = Instantiate(BeamBlast, transform.position + MuzzleLocation, Quaternion.identity); //Beam blast is instantiated, replacing the aiming laser
        yield return new WaitForSeconds(BeamBlastTimer);        //Keeps beam blast on screen for (BeamBlastTimer) seconds
        Destroy(BeamBlast);                                     //Destroy beam blast, end of firing process. 
    }

    //Note: Only fires once, then never does anything again. to fire again, a new beamweapon object must be instantiated. Other scripts that will farm patterns will take care of instantiating/destorying these.



    


}
