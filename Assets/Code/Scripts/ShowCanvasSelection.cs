using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShowCanvasSelection : MonoBehaviour
{
    public static GameObject canvasToShow;
    public PlayableDirector director;
    public GameObject mainMenuCanvas;

    //public static bool isTriggered = false;

    private bool _shownCanvas = true;
    private bool _mainMenuBtnPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state != PlayState.Playing && director.time <= 1.0f && MainMenuManager.triggerSelectedSelection && _shownCanvas)
        {
            _shownCanvas = false;
            canvasToShow.SetActive(true);
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
            MainMenuManager.triggerSelectedSelection = false;
        }
        //Debug.Log("director Time: " + director.time);
    }
    
    public void MainMenuButtonClicked()
    {
        canvasToShow.SetActive(false);
        director.ReversePlay();
        _mainMenuBtnPressed = true;
        _shownCanvas = true;
    }
}
