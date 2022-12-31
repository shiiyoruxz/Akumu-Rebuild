using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerSystem : MonoBehaviour
{
    public GameObject dialogues;
    public int patrolPhase = 1;
    public int dialPhase = 1;
    private String triggerAreaName;
    private String dialName;
    private GameObject dialChild;
    private float dialTotalTime;
    private String cutsceneTag;

    private CutsceneManager cutsceneMng;
    private DialogueSystem dialSys;
    public TaskAssignController taskCtrl;

    private void Start()
    {
        triggerAreaName = "dialArea";
        dialName = "dial";
        cutsceneTag = "cutscene";
        cutsceneMng = GetComponent<CutsceneManager>();
        
    }
    
    void OnTriggerEnter(Collider col)
    {
        dialChild = dialogues.transform.GetChild(0).gameObject.transform.GetChild(dialPhase - 1).gameObject;
        dialSys = dialogues.transform.GetChild(0).gameObject.transform.Find(dialChild.name).GetComponent<DialogueSystem>();
        dialTotalTime = (dialSys.lines.Length * dialSys.nextDialogueSpeed);
        if (col.gameObject.name == triggerAreaName+dialPhase.ToString())
        {
            StartCoroutine(dialogueAppearTime());
            StartCoroutine(taskCtrl.updateTask(dialPhase, dialTotalTime));
            dialPhase++;
        
        }

        if (col.gameObject.CompareTag(cutsceneTag))
        {
            Destroy(col.gameObject);
            cutsceneMng.cutsceneLoader();
            
        }
        
    }

    IEnumerator dialogueAppearTime()
    {
        yield return new WaitForSeconds(1.5f);
        dialChild.SetActive(true);
    }
    
}
