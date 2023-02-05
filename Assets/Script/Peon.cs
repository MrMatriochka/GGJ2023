using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peon : MonoBehaviour
{
    public List<Material> matList = new List<Material>();

    public float baseDeathTime;
    Player player;

    public float moveSpeed;
    public float runSpeed;
    public float walkRadius;
    public float runDistance;
    public float calmDistance;

    bool isWandering = true;
    NavMeshAgent agent;
    Animator animator;

    bool colidedMiniPlayer;
    MiniPlayer miniPlayer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        agent.speed = moveSpeed;
        agent.SetDestination(RandomNavMeshLocation());

        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = matList[Random.Range(0,matList.Count)];
    }

    private void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && isWandering)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance<runDistance)
        {
            isWandering = false;
            agent.speed = runSpeed;
            animator.SetBool("Run", true);
        }
        
        if(distance>calmDistance )
        {
            isWandering = true;
            agent.speed = moveSpeed;
            animator.SetBool("Run", false);
        }

        if(!isWandering)
        {
            agent.SetDestination( transform.position- player.transform.position);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("MiniPlayer"))
        {
            if(other.CompareTag("MiniPlayer"))
            {
                colidedMiniPlayer = true;
                miniPlayer = other.GetComponent<MiniPlayer>();
            }
            
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.parent = other.transform;


            StartCoroutine(Gangraine());
        }
    }

    IEnumerator Gangraine()
    {
        if(colidedMiniPlayer)
        {
            yield return new WaitForSeconds(baseDeathTime / miniPlayer.nerfedGangraine);
        }
        else
        {
            yield return new WaitForSeconds(baseDeathTime / player.nerfedGangraine);
        }
        Destroy(gameObject);
        yield return null;
    }

    Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition,out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
