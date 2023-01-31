using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 distance;
    private void Start()
    {
        distance =  transform.position - player.transform.position;
    }
    void Update()
    {
        transform.position = player.transform.position + distance;
    }
}
