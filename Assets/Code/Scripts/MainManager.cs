// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class MainManager : MonoBehaviour
// {
//     public static MainManager Instance;
//     private string sceneName;
//     private void Awake()
//     {
//         if (Instance != null)
//         {
//             Destroy(gameObject);
//             return;
//         }
//
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//         
//
//     }
//
//     private void Start()
//     {
//         Scene currentScene = SceneManager.GetActiveScene();
//         sceneName = currentScene.name;
//         // StartCoroutine(checkScene());
//     }
//     
//     // IEnumerator checkScene()
//     // {
//     //     yield return new WaitForSeconds(0.5f);
//     //     Scene currentScene = SceneManager.GetActiveScene();
//     //     
//     //     StartCoroutine(checkScene());
//     // }
//
//
//
//     
//     // IEnumerator checkScene()
//     // {
//     //     yield return new WaitForSeconds(0.5f);
//     //     Scene currentScene = SceneManager.GetActiveScene();
//     //     
//     //     if (currentScene.name != sceneName)
//     //     {
//     //         
//     //         
//     //         // Unload game scene and load cutscene scene
//     //         SceneManager.UnloadSceneAsync("SampleScene");
//     //         SceneManager.LoadSceneAsync("CutSceneScene", LoadSceneMode.Additive);
//     //
//     //         // Disable "FirstPersonControllerPrefab" object when scene changes
//     //         gameObject.transform.Find(disableGameobj[0]).gameObject.SetActive(false);
//     //
//     //         Debug.Log("Changed scene to cutscene");
//     //         
//     //         Destroy(GameObject.Find(disableGameobj[1]));
//     //         
//     //     }
//     //     else
//     //     {
//     //         // Destroy(GameObject.Find(disableGameobj[1]));
//     //         
//     //         // Enable "FirstPersonControllerPrefab" object when returning to game scene
//     //         gameObject.transform.Find(disableGameobj[0]).gameObject.SetActive(true);
//     //         Debug.Log("Back to game scene");
//     //     }
//     //
//     //     StartCoroutine(checkScene());
//     // }
//
//
// }
