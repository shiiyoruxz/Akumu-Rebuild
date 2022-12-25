using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    private FieldOfView AIView;
    private Animator anim;

    public GameObject wayPoints;
    public Transform[] patrolPoints;
    private float waitTime;
    private int currentPointIndex;

    private bool once;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AIView = GetComponent<FieldOfView>();
        waitTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        if (AIView.canSeePlayer)
        {
            agent.SetDestination(AIView.playerRef.transform.position);
        }
        else
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                agent.SetDestination(patrolPoints[currentPointIndex].position);
            }
            else
            {
                if (!once)
                {
                    once = true;
                    StartCoroutine(Wait());
                }
               
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        once = false;
    }
    
}
