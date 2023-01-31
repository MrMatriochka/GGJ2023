using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peon : MonoBehaviour
{
    public float baseDeathTime;
    Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            transform.parent = other.transform;
            StartCoroutine(Gangraine());
        }
    }

    IEnumerator Gangraine()
    {
        yield return new WaitForSeconds(baseDeathTime/player.gangraine);
        Destroy(gameObject);
        yield return null;
    }
}
