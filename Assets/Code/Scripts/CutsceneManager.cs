using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private CutsceneLoader csLoader;
    private void Start()
    {
        csLoader = GetComponent<CutsceneLoader>();
    }
    
    public void cutsceneLoader()
    {
        
        SceneManager.LoadScene(2);

    }
}
