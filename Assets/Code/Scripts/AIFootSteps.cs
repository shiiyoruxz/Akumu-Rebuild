using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFootSteps : MonoBehaviour
{
    public AudioSource footStepsSound, sprintSound;
    public NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.speed == 2.2f){
            footStepsSound.enabled = true;
            sprintSound.enabled = false;
        }
        
        if (agent.speed >= 4.0f)
        {
            footStepsSound.enabled = false;
            sprintSound.enabled = true;
        }
    }
}
