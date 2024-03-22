using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadbehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); // setting the selection box to inactive at start
        gameObject.transform.rotation = Quaternion.Euler(90.0f, gameObject.transform.rotation.y, gameObject.transform.rotation.z); // rotating the selection box to observer view
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
