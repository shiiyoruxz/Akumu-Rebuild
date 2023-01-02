using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool playerIsDead = false;
    public static bool playerWantRetry = false;

    private void Update()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.None; // unlocks the cursor
    }

    public void playerRetry()
    {
        //Gameover gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        //Jumpscare gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //Ghost prefabs gameobject
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.transform.position = gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent
            .GetComponent<AIController>().patrolPoints[AIController.patrolPhase].transform
            .GetChild(AIController.currentPointIndex).transform.position;
        playerWantRetry = true;
        Cursor.visible = false;
    }

    public void playerReturnMainMenu()
    {
        //Gameover gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        //Jumpscare gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        playerIsDead = false;
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

    IEnumerator resetGhostPosition()
    {
        
        GameObject ghost = gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.gameObject;
        ghost.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        ghost.transform.position = ghost.GetComponent<AIController>().patrolPoints[AIController.patrolPhase].transform.GetChild(AIController.currentPointIndex).transform.position;
        ghost.SetActive(true);
    }
    
}
