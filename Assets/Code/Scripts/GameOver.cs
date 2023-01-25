using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Update()
    {
        
    }

    public void playerRetry()
    {
        Time.timeScale = 1;
        //Gameover gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        //Jumpscare gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        AIController.playerWantRetry = true;
        AIController.playerIsDead = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void playerReturnMainMenu()
    {
        Time.timeScale = 1;
        MainMenuManager.returnMainMenu = true;
        FlashlightManager.firstBattery = true;
        FadeInBtn.showMenuBtn = true;
        Inventory.numBattery = 0;
        //Gameover gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        //Jumpscare gameobject set to false
        gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        AIController.playerIsDead = false;
        Cursor.visible = true;
        CutsceneManager.currentCutscene = 0;
        SceneManager.LoadScene(0);
        MainMenuManager.DestroyOnReturnMenu();
    }
}
