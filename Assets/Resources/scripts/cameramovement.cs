using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class cameramovement : MonoBehaviour
{

    public float movementspeed; // the movement speed of the camera
    public int scrollspeed = 5; // the scroll speed of the camera
    public int scrollmaxlimit = 50; // the max scroll limit of the camera
    public int scrollminlimit = 10; // the min scroll limit of the camera

    public Vector3 newposition;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.fieldOfView = 57; // the base field of the camera
        newposition = transform.position; // the position of the camera
    }

    // Update is called once per frame
    void Update()
    {

        // called every tick to update the camera
        scrollcamera();
        movecamera();
    }
    
    // function to scroll the camera
    void scrollcamera()
    {

        float percentage = (Camera.main.fieldOfView / 179 * 100);

        if (percentage > scrollminlimit ) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // scroll down
            {
                Camera.main.fieldOfView -= scrollspeed;
            }
        }

        if (percentage < scrollmaxlimit)
        {

            if (Input.GetAxis("Mouse ScrollWheel") < 0) // scroll up
            {
                Camera.main.fieldOfView += scrollspeed;
            }

        }
    }



    // function to move the camera
    void movecamera()
    {
        float facing = Camera.main.transform.rotation.eulerAngles.y;

        if (Input.mousePosition.y > Screen.height - 4f) // move screen up
        {
            newposition += (transform.forward * movementspeed);
            transform.position = newposition;
        }

        if (Input.mousePosition.y < 4f) // move screen down
        {
            newposition += (transform.forward * -movementspeed);
            transform.position = newposition;
        }

        if (Input.mousePosition.x > Screen.width - 4f) // move screen right
        {
            newposition += (transform.right * movementspeed);
            transform.position = newposition;
        }

        if (Input.mousePosition.x < 4f) // move screen left
        {
            newposition += (transform.right * -movementspeed);
            transform.position = newposition;
        }
    }
}
