using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public float size;
    [Min(0)]public float sizeInclinaison;
    [Min(1)] public float sizeMax =1;
    [HideInInspector] public float gangraine;
    
    [Min(0)] public float gragnaineFirstInclinaison;
    [Range(0,1)] public float gragnaineSecondInclinaison;
    [HideInInspector] public float nerfGangraine;
    [Min(0)] public float nerfGangraineInclinaison;
    [Min(1)] public float nerfGangraineMax;
    [HideInInspector] public float nerfedGangraine;
    [Min(1)] public float nerfPower;
    float elapsedTime;

    public float speed;
    Rigidbody rb;

    Vector3 startSlingshot;
    Vector3 endSlingshot;
    bool canSlingshot;

    public GameObject miniPlayer;
    int miniPlayerIndex = 0;
    
    public GameObject sizeBarObj;
    private SizeBar sizeBar;

    public float smoothResize;
    Material mat;

    public float deathTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sizeBar = sizeBarObj.GetComponent<SizeBar>();
        sizeBar.SetMaxSize(15);
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        

        //Get size
        size = transform.childCount;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * ((sizeInclinaison * Mathf.Log((size + 2) * sizeMax)) + 1), smoothResize);
        //transform.localScale = Vector3.one * ((sizeInclinaison * Mathf.Log((size + 2)* sizeMax)  ) + 1);


        //Get nerfGangraine
        nerfGangraine = (nerfGangraineInclinaison * Mathf.Log((size + 2) * nerfGangraineMax))+1;
        
        //Get Gangraine with time
        gangraine = Mathf.Pow(gragnaineFirstInclinaison+1,elapsedTime* gragnaineSecondInclinaison)+1;
        mat.SetFloat("_Gangraine", gangraine/500);
        //Get NerfedGangraine
        nerfedGangraine = gangraine / (nerfGangraine*nerfPower);

        sizeBar.SetSize(size);

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

                splitedObject.GetComponent<MiniPlayer>().sizeInclinaison = sizeInclinaison;
                splitedObject.GetComponent<MiniPlayer>().sizeMax = sizeMax;
                splitedObject.GetComponent<MiniPlayer>().gragnaineFirstInclinaison = gragnaineFirstInclinaison;
                splitedObject.GetComponent<MiniPlayer>().gragnaineSecondInclinaison = gragnaineSecondInclinaison;
                splitedObject.GetComponent<MiniPlayer>().nerfGangraineInclinaison = nerfGangraineInclinaison;
                splitedObject.GetComponent<MiniPlayer>().nerfGangraineMax = nerfGangraineMax;
                splitedObject.GetComponent<MiniPlayer>().nerfPower = nerfPower;
                splitedObject.GetComponent<MiniPlayer>().deathTimer = deathTimer;

                splitedObject.GetComponent<Rigidbody>().AddForce(movement * speed);
                splitedObject.GetComponent<MiniPlayer>().index = miniPlayerIndex;
                miniPlayerIndex++;

                int iterrationNb = Mathf.RoundToInt(transform.childCount / 2);
                for (int i = 0; i < iterrationNb; i++)
                {
                    transform.GetChild(1).parent = splitedObject.transform;
                }
            }
            canSlingshot = false;

            if(size == 0)
            {
                StartCoroutine(DeathTimer());
            }
            else { StopCoroutine(DeathTimer()); }
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);
        print("GameOver");
    }
}
