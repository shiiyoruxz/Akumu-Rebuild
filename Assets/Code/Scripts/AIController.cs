using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    private FieldOfView AIView;
    private Animator anim;
    public GameObject jumpScare;
    public GameObject gameOver;
    
    public Transform[] patrolPoints;
    private float waitTime = 2.0f;
    private float chaseTime;
    private int currentPointIndex;

    private bool once;
    private string targetTag = "Player";
    private Vector3 destination;
    
    private bool inAttackRange;
    private float speed;
    public bool isPatrol;
    public bool isChase;
    public bool isAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        AIView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIView.canSeePlayer)
        {
            isChase = true;
            if (isChase)
            {
                chaseTime = 3.0f;
                if (chaseTime > 0)
                {
                    Chasing();
                    chaseTime -= Time.deltaTime;
                }
                else
                {
                    isChase = false;
                }
            }

        }
        else
        {
            if (chaseTime > 0)
            {
                Chasing();
                chaseTime -= Time.deltaTime;
            }
            else
            {
                Patrolling();
                isChase = false;
            }
        }

        StartCoroutine(setDestination());
        anim.SetFloat("Speed", agent.velocity.magnitude);

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints[EventTriggerSystem.dialPhase].childCount)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        once = false;
    }

    private void Patrolling()
    {
        agent.speed = 2.0f;
        if (Vector3.Distance(transform.position, destination) > 1.0f)
        {
            destination = patrolPoints[EventTriggerSystem.dialPhase].GetChild(currentPointIndex).position;
            agent.destination = destination;
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
    
    private void Chasing()
    {
        agent.speed = 3.8f;

        if (Vector3.Distance(transform.position, AIView.playerRef.transform.position) > 1.0f)
        {
            destination = AIView.playerRef.transform.position;
            agent.destination = destination;
            
        }
        
        if (Vector3.Distance(transform.position, AIView.playerRef.transform.position) < 1.0f)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }

    }
    
    // private void Attacking()
    // {
    //     if (Vector3.Distance(transform.position, AIView.playerRef.transform.position) < 1.0f)
    //     {
    //         triggerJumpScare();
    //         StartCoroutine(triggerGameOver());
    //     }
    // }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag(targetTag))
        {
            triggerJumpScare();
            StartCoroutine(triggerGameOver());
        }
    }

    IEnumerator setDestination()
    {
        yield return new WaitForSeconds(waitTime);
        destination = patrolPoints[EventTriggerSystem.dialPhase].GetChild(currentPointIndex).position;
    }

    void triggerJumpScare()
    {
        jumpScare.SetActive(true);
    }

    IEnumerator triggerGameOver()
    {
        yield return new WaitForSeconds(2.0f);
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    

}