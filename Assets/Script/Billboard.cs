using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camera;
    public GameObject player;
    private Vector3 distance;
    public Vector3 offset;
    private void Start()
    {
        distance =  transform.position - player.transform.position;
    }
    void Update()
    {
        transform.position = player.transform.position + distance + offset;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
