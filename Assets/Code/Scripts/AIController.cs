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
    public static int currentPointIndex = 0;

    private bool once;
    private string targetTag = "Player";
    private Vector3 destination;
    
    private bool inAttackRange;
    private float speed;
    public bool isPatrol;
    public static bool isChase;
    public bool isAttack;

    public int previousPatrolPhase;
    public static int patrolPhase = 0;
    
    public static bool playerIsDead = false;
    public static bool playerWantRetry = false;
    private bool _playOnce = true;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        AIView = GetComponent<FieldOfView>();

        previousPatrolPhase = patrolPhase;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPatrolPhase != patrolPhase)
        {
            patrolPhase = AIController.patrolPhase;
        }
        
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
        if (currentPointIndex + 1 < patrolPoints[patrolPhase].childCount)
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
        FieldOfView.playSpotted = false;
        agent.speed = 2.2f;
        if (currentPointIndex >= 0 && currentPointIndex < patrolPoints[patrolPhase].transform.childCount)
        {
            if (Vector3.Distance(transform.position, destination) > 1.0f)
            {
                destination = patrolPoints[patrolPhase].transform.GetChild(currentPointIndex).position;
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
        else
        {
            destination = patrolPoints[patrolPhase].transform.GetChild(currentPointIndex).position;
            agent.destination = destination;
        }
        
    }
    
    private void Chasing()
    {
        // AudioManager.Instance.PlaySFX("Spotted");
        agent.speed = 4.0f;

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

        StartCoroutine(chaseSpeedUp());

    }

    IEnumerator chaseSpeedUp()
    {
        yield return new WaitForSeconds(2.5f);
        agent.speed = 4.7f;
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
            playerIsDead = true;
            triggerJumpScare();
            if (jumpScare.activeSelf == true)
            {
                AudioManager.Instance.PlaySFX("JumpScare");
                _playOnce = false;
            }
            StartCoroutine(triggerGameOver());
        }
    }

    IEnumerator setDestination()
    {
        yield return new WaitForSeconds(waitTime);
        destination = patrolPoints[patrolPhase].GetChild(currentPointIndex).position;
    }

    void triggerJumpScare()
    {
        jumpScare.SetActive(true);
        resetGhostPosition();
    }

    IEnumerator triggerGameOver()
    {
        yield return new WaitForSeconds(2.0f);
        jumpScare.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // unlocks the cursor
    }
    
    public void resetGhostPosition()
    {
        GameObject ghost = gameObject;
        ghost.transform.position = ghost.GetComponent<AIController>().patrolPoints[AIController.patrolPhase].transform.GetChild(AIController.currentPointIndex).transform.position;
    }
}