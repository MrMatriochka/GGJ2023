using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public Camera camObj;

    private void Awake()
    {
        camObj = Camera.main;
        if (camObj != null) 
            cam = camObj.transform;
    }
    
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
