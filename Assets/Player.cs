using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 speed = new Vector2(50, 50);

    //private float moveSpeed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //var direction = new Vector3(horizontalInput, verticalInput, 0);

        //Debug.Log(horizontalInput);
        //Debug.Log(verticalInput);

        //transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
