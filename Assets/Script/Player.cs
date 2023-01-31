using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float size;
    public float gangraine;

    public float speed;
    Rigidbody rb;

    Vector3 startSlingshot;
    Vector3 endSlingshot;
    bool canSlingshot;

    public GameObject miniPlayer;
    int miniPlayerIndex = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        size = transform.childCount+1;
        gangraine = transform.childCount + 1;
        transform.localScale = Vector3.one * size;


        //slingShot
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    startSlingshot = Input.mousePosition;
                    canSlingshot = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) )
        {
            if(transform.childCount > 1 && canSlingshot)
            {
                endSlingshot = Input.mousePosition;
                Vector3 movement = new Vector3(startSlingshot.x - endSlingshot.x, 0.0f, startSlingshot.y - endSlingshot.y);

                GameObject splitedObject = Instantiate(miniPlayer,transform.position,Quaternion.identity);
                splitedObject.GetComponent<Rigidbody>().AddForce(movement * speed);
                splitedObject.GetComponent<MiniPlayer>().index = miniPlayerIndex;
                miniPlayerIndex++;

                int iterrationNb = Mathf.RoundToInt(transform.childCount / 2);
                for (int i = 0; i < iterrationNb; i++)
                {
                    transform.GetChild(0).parent = splitedObject.transform;
                }
            }
            canSlingshot = false;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}
