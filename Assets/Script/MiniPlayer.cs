using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    public float size;
    public float gangraine;

    public float speed;
    Rigidbody rb;

    Vector3 startSlingshot;
    Vector3 endSlingshot;
    bool canSlingshot;
    bool canFusion;
    public int index;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Initialize());
    }

    private void Update()
    {
        size = transform.childCount + 1;
        gangraine = transform.childCount + 1;
        transform.localScale = Vector3.one * size;


        //slingShot
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    startSlingshot = Input.mousePosition;
                    canSlingshot = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (canSlingshot)
            {
                endSlingshot = Input.mousePosition;
                Vector3 movement = new Vector3(startSlingshot.x - endSlingshot.x, 0.0f, startSlingshot.y - endSlingshot.y);
                rb.AddForce(movement * speed);
            }
            canSlingshot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(canFusion)
        {
            if (collision.collider.CompareTag("Player"))
            {
                int iterrationNb = transform.childCount;
                for (int i = 0; i < iterrationNb; i++)
                {
                    transform.GetChild(0).parent = collision.collider.transform;
                }

                Destroy(gameObject);
            }

            if (collision.collider.CompareTag("MiniPlayer") && index > collision.gameObject.GetComponent<MiniPlayer>().index)
            {
                int iterrationNb = transform.childCount;
                for (int i = 0; i < iterrationNb; i++)
                {
                    transform.GetChild(0).parent = collision.collider.transform;
                }

                Destroy(gameObject);
            }
        }
        
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1f);
        canFusion = true;
        yield return null;
    }
}
