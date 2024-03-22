using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbarsystem : MonoBehaviour
{
    [SerializeField] Transform minion;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(0f, minion.GetComponent<BoxCollider>().transform.localScale.y, 0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
