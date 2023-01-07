using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));  //Gets the bounds for the camera
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;   //gets the width of whatever object this script is attached to (Important so object model doesnt clip out of bounds)
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;  //same as above but with height
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //The following code block makes sure to keep the object withing the bounds of the given camera bounds from earlier.
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight - 2);
        transform.position = viewPos;

    }
}
