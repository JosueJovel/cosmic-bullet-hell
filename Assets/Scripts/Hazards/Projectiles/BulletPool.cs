using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;       //This script "pools" objects, so that they can be used and reused. This is more efficient than instantiating and deleting many objects over time
    [SerializeField] private GameObject pooledBullet;  //Represents the game object/prefab of bullet (The bullet prefab can be attached to this script in the unity inspector)
    private bool notEnoughBulletsInPool = true;       //Used to check if pool needs more bullets
    private List<GameObject> bullets;               //This is the actual pool of bullets being used
    
    private void Awake() {
        bulletPoolInstance = this;  //This game object is assigned to a public variable, so that other scripts may access it, such as taking bullets out of this pool
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet(){  //This is what's used by other scripts to get a bullet out of the bullet pool
        if (bullets.Count > 0) {  //Only fetch a bullet if the pool is not empty
            for (int i = 0; i < bullets.Count; i++)  //Then, the pool of bullets is searched through until a single inactive bullet is found.
            {
                if (!bullets[i].activeInHierarchy)   
                {
                    return bullets[i];    //Once an inactive bullet is found, it is returned
                }
            }
        }

        if (notEnoughBulletsInPool)  //This triggers if the pool is empty, or there are not inactive bullets in the pool
        {
            //Creates a new bullet, ensures it starts inactive, and adds it to the pool
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);    

            //Finally, that new bullet is returned to whatever script called this getter.
            return bul;
        }

        return null;
    }

    // Update is called once per frame
    
}
