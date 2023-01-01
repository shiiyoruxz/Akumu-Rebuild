using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private string sceneName;

    private GameObject destroyDoor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        

    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        StartCoroutine(checkScene());
        
        
    }

    private void Update()
    {
        if (CutsceneManager.currentCutscene == 1)
        {
            gameObject.transform.GetChild(4).transform.gameObject.SetActive(true);
        }
    }

    IEnumerator checkScene()
    {
        yield return new WaitForSeconds(0.1f);
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != sceneName)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).gameObject.SetActive(false);

        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }
        StartCoroutine(checkScene());
    }
    
}
