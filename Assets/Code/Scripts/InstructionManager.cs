using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionCanvas;
    public PlayableDirector director;
    public GameObject mainMenuCanvas;

    private bool _showInstructions = true;
    private bool _mainMenuBtnPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state != PlayState.Playing && director.time <= 1.0f && MainMenuManager.instrucPressed && _showInstructions)
        {
            _showInstructions = false;
            instructionCanvas.SetActive(true);
        }

        if (director.time <= 5.0f && _mainMenuBtnPressed == true)
        {
            Debug.Log("Helloasdasdassd");

            // director.timeUpdateMode = DirectorUpdateMode.GameTime;
            // director.Evaluate();
            // director.Pause();

            TimelineExtension.stopPlaying = true;
            mainMenuCanvas.SetActive(true);
            _mainMenuBtnPressed = false;
            FadeInBtn.showMenuBtn = true;
            FadeInBtn.finishHide = false;
            MainMenuManager.instrucPressed = false;
        }
        //Debug.Log("director Time: " + director.time);
    }
    
    public void MainMenuButtonClicked()
    {
        instructionCanvas.SetActive(false);
        director.ReversePlay();
        _mainMenuBtnPressed = true;
        _showInstructions = true;
    }
}
