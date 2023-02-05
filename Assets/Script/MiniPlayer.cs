using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    [HideInInspector] public float size;
    [Min(0)] public float sizeInclinaison;
    [Min(1)] public float sizeMax = 1;
    [HideInInspector] public float gangraine;

    [Min(0)] public float gragnaineFirstInclinaison;
    [Range(0, 1)] public float gragnaineSecondInclinaison;
    [HideInInspector] public float nerfGangraine;
    [Min(0)] public float nerfGangraineInclinaison;
    [Min(1)] public float nerfGangraineMax;
    [HideInInspector] public float nerfedGangraine;
    public float nerfPower;
    float elapsedTime;

    public float speed;
    Rigidbody rb;

    Vector3 startSlingshot;
    Vector3 endSlingshot;
    bool canSlingshot;
    bool canFusion;
    public int index;
    Material mat;
    public float deathTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Initialize());

        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        //Get size
        size = transform.childCount;
        transform.localScale = Vector3.one * ((sizeInclinaison * Mathf.Log((size + 2) * sizeMax)) + 1);


        //Get nerfGangraine
        nerfGangraine = (nerfGangraineInclinaison * Mathf.Log((size + 2) * nerfGangraineMax)) + 1;

        //Get Gangraine with time
        gangraine = Mathf.Pow(gragnaineFirstInclinaison + 1, elapsedTime * gragnaineSecondInclinaison) + 1;
        mat.SetFloat("_Gangraine", gangraine / 500);
        //Get NerfedGangraine
        nerfedGangraine = gangraine / (nerfGangraine * nerfPower);

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

        if (size == 0)
        {
            StartCoroutine(DeathTimer());
        }
        else { StopCoroutine(DeathTimer()); }
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
        yield return new WaitForSeconds(0.25f);
        canFusion = true;
        GetComponent<Collider>().enabled = true;
        yield return null;
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}
