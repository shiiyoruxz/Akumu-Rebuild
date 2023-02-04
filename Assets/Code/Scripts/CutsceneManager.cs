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
    
    private string _sceneName = "CutSceneScene";
    public static bool playInGameBGM = false;

    private void Start()
    {
        csManager = this.gameObject;
        playDirector = csManager.transform.GetChild(currentCutscene).GetComponent<PlayableDirector>();
        for (int i = 0; i < csManager.transform.childCount; i++)
        {
            csManager.transform.GetChild(i).gameObject.SetActive(false);
        }

        csManager.transform.GetChild(currentCutscene).gameObject.SetActive(true);

        if (currentCutscene == 1)
        {
            Debug.Log(GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(2).gameObject.name);
            GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == _sceneName && playInGameBGM)
        {
            AudioManager.Instance.PlayMusic("InGameBGM");
            playInGameBGM = false;
        }
        
        if (playDirector.time >= playDirector.duration-1.0f)
        {
            isCompleted = true;
        }

        if (isCompleted)
        {
            if (GameObject.Find("NoDestroyObject") != null)
            {
                GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("NoDestroyObject").gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            isCompleted = false;
            SceneManager.LoadScene(2);
        }
    }
    

}
