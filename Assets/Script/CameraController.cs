using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject player;
    //Vector3 distance;

    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;
    public float upResizeFactor;

    private void Start()
    {
        
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shake(1,0.1f));
        }
    }
    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        float size = target.GetComponent<Player>().size;
        Vector3 targetPos = target.position + offset + Vector3.up*Mathf.Pow(size, upResizeFactor) ;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,targetPos, smoothSpeed);

        transform.position = smoothFollow;
        //transform.LookAt(target);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsedTime = 0f;

        while (elapsedTime<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, 0, z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }
}


