using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource footStepsSound, sprintSound;
    public GameObject firstPersonControllerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPersonControllerPrefab.GetComponent<FirstPersonController>().enabled)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    footStepsSound.enabled = false;
                    sprintSound.enabled = true;
                }
                else
                {
                    footStepsSound.enabled = true;
                    sprintSound.enabled = false;
                }
            }
            else
            {
                footStepsSound.enabled = false;
                sprintSound.enabled = false;
            }
        }
    }
}
