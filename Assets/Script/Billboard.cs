using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public GameObject camObj;
    public GameObject player;
    
    private Vector3 distance;
    public Vector3 offset;
    
    private void Awake()
    {
        distance =  transform.position - player.transform.position;
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.transform;
    }
    void Update()
    {
        transform.position = player.transform.position + distance + offset;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
