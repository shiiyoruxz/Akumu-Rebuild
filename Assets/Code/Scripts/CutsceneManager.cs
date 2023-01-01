using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject csManager;

    public PlayableDirector playDirector;
    public static int currentCutscene = 0;
    public static bool isCompleted = false;

    private void Start()
    {
        csManager = this.gameObject;
        playDirector = csManager.transform.GetChild(currentCutscene).GetComponent<PlayableDirector>();
        for (int i = 0; i < csManager.transform.childCount; i++)
        {
            csManager.transform.GetChild(i).gameObject.SetActive(false);
        }
        csManager.transform.GetChild(currentCutscene).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (playDirector.time >= playDirector.duration-1.0f)
        {
            isCompleted = true;
        }

        if (isCompleted)
        {
            isCompleted = false;
            SceneManager.LoadScene(2);
        }
    }
    

}
