// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class EventTriggerSystem : MonoBehaviour
// {
//     public GameObject dialogues;
//     public static int patrolPhase = 0;
//     public static int dialPhase = 0;
//     public static int taskPhase = 0;
//
//     public float pickUpDistance = 1.0f;
//     
//     private String cutsceneTag;
//     private String dialogTag;
//     private String itemTag;
//
//     private bool dialogIsTriggered = false;
//     
//     private GameObject dialogTriggerArea;
//     private GameObject cutsceneTriggerArea;
//     private GameObject dialogCanvas;
//     private GameObject taskCanvas;
//
//     private CutsceneManager cutsceneMng;
//     private DialogueSystem dialSys;
//
//     private void Start()
//     {
//         cutsceneTag = "cutscene";
//         dialogTag = "dialog";
//         itemTag = "Collectable";
//         dialogTriggerArea = GameObject.Find("DialogueTriggerArea");
//         cutsceneTriggerArea = GameObject.Find("CutsceneTriggerArea");
//         dialogCanvas = gameObject.transform.GetChild(2).transform.GetChild(0).gameObject;
//         taskCanvas = gameObject.transform.GetChild(3).transform.GetChild(0).gameObject;
//         // StartCoroutine(dialogueAppearTime());
//     }
//
//     void dialogUpdate()
//     {
//         dialogIsTriggered = true;
//         dialogTriggerArea.transform.GetChild(dialPhase).gameObject.SetActive(false);
//         StartCoroutine(dialogueAppearTime());
//     }
//
//     IEnumerator dialogueAppearTime()
//     {
//         yield return new WaitForSeconds(1.5f);
//         dialogCanvas.transform.GetChild(dialPhase).gameObject.SetActive(true);
//         dialPhase++;
//         dialogIsTriggered = false;
//
//     }
//     
//     IEnumerator updateTask()
//     {
//         yield return new WaitForSeconds(7.0f);
//
//         if (taskPhase > 0)
//         {
//             taskCanvas.transform.GetChild(taskPhase-1).gameObject.SetActive(false);
//         }
//         taskCanvas.transform.GetChild(taskPhase).gameObject.SetActive(true);
//         
//         taskPhase++;
//     }
//
//     
// }