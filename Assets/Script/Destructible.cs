using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float sizeToDestroy;
    GameObject cam;
    public bool isBuilding;
    public bool isCar;
    public float duration;

    public GameObject vfxBig;
    public GameObject vfx;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if(other.collider.GetComponent<Player>().size>sizeToDestroy)
            {
                StartCoroutine(DestroyObject());
            }
            
        }
        if (other.collider.CompareTag("MiniPlayer"))
        {
            if (other.collider.GetComponent<MiniPlayer>().size > sizeToDestroy)
            {
                StartCoroutine(DestroyObject());
            }

        }
    }

    IEnumerator DestroyObject()
    {
        StartCoroutine(cam.GetComponent<CameraController>().Shake(1, 0.1f));
        if (isCar)
        {
            Destroy(gameObject);
            Instantiate(vfx, transform.position, Quaternion.identity);
        }
        if (isBuilding)
        {
            Instantiate(vfxBig, transform.position, Quaternion.identity);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, (1*duration)*Time.deltaTime);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            Destroy(gameObject);
            
        }
        
    }
}
