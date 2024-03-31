using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movementtest : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && this.transform.GetComponent<minion>().ismoveable)
        {
            // Detect W and S keys for forward and backward movement
            if (Input.GetKey(KeyCode.W))
            {
                verticalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalInput = -1f;
            }

            // Detect A and D keys for left and right movement
            if (Input.GetKey(KeyCode.D))
            {
                horizontalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontalInput = -1f;
            }

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
