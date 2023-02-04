using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public static bool runOnce = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runOnce)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            runOnce = false;
        }
    }

    public void returnMenuCLicked()
    {
        MainMenuManager.returnMainMenu = true;
        FlashlightManager.firstBattery = true;
        FadeInBtn.showMenuBtn = true;
        Inventory.numBattery = 0;
        CutsceneManager.currentCutscene = 0;
        PlayerInteraction.surpriseToiletVent = true;

        SceneManager.LoadScene(0);
    }
}
