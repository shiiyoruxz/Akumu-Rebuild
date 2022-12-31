using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{
    private GameObject cutsceneObj;

    public PlayableDirector[] timeline;

    public static bool timelineIsCompleted = false;
    
    public static int num = 0;

    private void Start()
    {
        for (int n = 0; n < timeline.Length; n++)
        {
            timeline[n].Stop();
        }
        Debug.Log("START");
        timeline[num].Play();
    }

    private void Update()
    {
        if (!timeline[num].transform.gameObject.activeSelf)
        {
            num++;
            // Debug.Log(num);
            timelineIsCompleted = true;
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Time.timeScale = 0;
            // timelineIsCompleted = false;
        }
    }
    
}
