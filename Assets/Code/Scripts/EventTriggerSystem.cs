using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTriggerSystem : MonoBehaviour
{
    public GameObject dialogues;
    public static int patrolPhase = 0;
    public static int dialPhase = 0;
    public static int taskPhase = 0;

    public float pickUpDistance = 1.0f;
    
    private String cutsceneTag;
    private String dialogTag;
    private String itemTag;

    private bool dialogIsTriggered = false;

    private GameObject destroyDoor;
    private GameObject dialogTriggerArea;
    private GameObject cutsceneTriggerArea;
    private GameObject dialogCanvas;
    private GameObject taskCanvas;
    public GameObject inventory;

    private static bool isHide = false;
    private static Transform temp_position;
    
    private CutsceneManager cutsceneMng;
    private DialogueSystem dialSys;

    private void Start()
    {
        cutsceneTag = "cutscene";
        dialogTag = "dialog";
        itemTag = "Collectable";
        dialogTriggerArea = GameObject.Find("DialogueTriggerArea");
        cutsceneTriggerArea = GameObject.Find("CutsceneTriggerArea");
        dialogCanvas = gameObject.transform.GetChild(2).transform.GetChild(0).gameObject;
        taskCanvas = gameObject.transform.GetChild(3).transform.GetChild(0).gameObject;
        // StartCoroutine(dialogueAppearTime());
    }

    private void Update()
    {
        MapInteraction();
        if (isHide)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                gameObject.transform.position = temp_position.position;
                gameObject.transform.Translate(0, -1.0f, 0.5f);
                // gameObject.GetComponent<CapsuleCollider>().enabled = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<FirstPersonController>().playerCanMove = true;
                isHide = false;
            }
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == dialogTriggerArea.transform.GetChild(dialPhase).name)
        {
            if (dialPhase == 0)
            {
                StartCoroutine(updateTask());
            }
            dialogUpdate();
        }

    }

    void dialogUpdate()
    {
        dialogIsTriggered = true;
        dialogTriggerArea.transform.GetChild(dialPhase).gameObject.SetActive(false);
        StartCoroutine(dialogueAppearTime());
    }

    IEnumerator dialogueAppearTime()
    {
        yield return new WaitForSeconds(1.5f);
        dialogCanvas.transform.GetChild(dialPhase).gameObject.SetActive(true);
        dialPhase++;
        dialogIsTriggered = false;

    }
    
    IEnumerator updateTask()
    {
        yield return new WaitForSeconds(7.0f);

        if (taskPhase > 0)
        {
            taskCanvas.transform.GetChild(taskPhase-1).gameObject.SetActive(false);
        }
        taskCanvas.transform.GetChild(taskPhase).gameObject.SetActive(true);
        
        taskPhase++;
    }
    
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag(cutsceneTag))
        {
            if (Input.GetKey(KeyCode.E))
            {
                CutsceneManager.currentCutscene++;
                Destroy(col.gameObject);
                SceneManager.LoadScene(1);
                StartCoroutine(dialogueAppearTime());
                StartCoroutine(updateTask());
            }
            
            if (CutsceneManager.currentCutscene == 1)
            {
                destroyDoor = GameObject.FindWithTag("destroyDoor");
                destroyDoor.transform.localPosition = new Vector3(0, 0.015f, -1.352f);
                destroyDoor.transform.localRotation = Quaternion.Euler(-89.386f, 0, 0);
            }

        }

    }
    
    void MapInteraction()
    {
        RaycastHit hit;

        // Map Interaction
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out hit, pickUpDistance))
        {
            // Check if the hit object has the "PickUp" tag
            if (hit.collider.tag == "Collectable")
            {
                // Pick up the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("COLLECTED");
                }
            }

            if (hit.collider.tag == "Battery")
            {
                // Pick up the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Battery collected");
                    StartCoroutine(dialogueAppearTime());
                    StartCoroutine(updateTask());
                    hit.collider.gameObject.SetActive(false);
                    hit.collider.gameObject.transform.SetParent(inventory.transform);
                }
            
            }

            if (hit.collider.tag == "Locker")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!isHide)
                    {
                        Debug.Log("Hide");
                        temp_position = gameObject.transform;
                        gameObject.transform.position = hit.collider.gameObject.transform.position;
                        gameObject.transform.Translate(0, 1.0f, 0);
                        // gameObject.GetComponent<CapsuleCollider>().enabled = false;
                        gameObject.GetComponent<Rigidbody>().useGravity = false;
                        gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        gameObject.GetComponent<FirstPersonController>().playerCanMove = false;
                        isHide = true;
                    }
                }


            }

            
            
        }
        
        
    }
    
}