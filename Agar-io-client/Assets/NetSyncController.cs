using System.Collections;
using System.Collections.Generic;
using Core.Net;
using UnityEngine;

public class NetSyncController : MonoBehaviour
{
    private float _movementSpd = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x * Time.deltaTime * _movementSpd, y * Time.deltaTime * _movementSpd, 0);
    }
}
