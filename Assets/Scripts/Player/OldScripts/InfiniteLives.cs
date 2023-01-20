using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLives : MonoBehaviour
{

    //This script is to give infinite lives to the player for when he/she dies. Doesn't work yet... I think it needs to be added to a more global object
    //because when the player dies, there's no way for this script to run. Essentially, this script is destroyed when the player dies.

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Instantiate(Player, new Vector3(0, -1, 0), Quaternion.identity);
        }
    }
}
