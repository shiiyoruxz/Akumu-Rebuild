using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class MainMenuManager : MonoBehaviour
{
    private bool _isQuit = false;
    private bool _triggerLoadingScreen = false;
    private float _timer = 0;
    
    private string _scenelToLoad;

    private string[] _toolTipsText = { "Press [F] to turn the flashlight ON/OFF", 
                                        "Press [ESC] to open in-game menu or pause the game",
                                        "Use [W], [A], [S], [D] to move your character around",
                                        "Press [E] to interact with objects",
                                        "Press [R] to reload battery to the flashlight",
                                        "Press [Shift] to sprint your character" };

    public TextMeshProUGUI targetText;
    public PlayableDirector director;
    public AudioClip buttonClickedSound;
    public GameObject loadingScreen;
    public GameObject mainMenuCanvas;
    public GameObject mainMenuVCam;
    
    public List<GameObject> canvasSelectionList = new List<GameObject>();

    public static bool triggerSelectedSelection = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_triggerLoadingScreen) 
        {
            loadingScreen.transform.GetChild(0).GetChild(1).gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
            SwapToolTipsText(5.0f);
        }

        if (triggerSelectedSelection && mainMenuCanvas.activeSelf == false)
        {
            director.Resume();

            if (director.initialTime == 5.0f)
            {
                director.initialTime = 0.0f;
                director.Play();
            }
        }
    }

    void PlayButtonClickedSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = buttonClickedSound;
        audio.Play();
    }

    void SwapToolTipsText(float seconds)
    {
        _timer += Time.deltaTime;
        if (_timer >= seconds)
        {
            int randomIndex = UnityEngine.Random.Range(0, _toolTipsText.Length);
            targetText.text = _toolTipsText[randomIndex];

            _timer = 0;
        }
    }

    IEnumerator SwitchScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        if (_isQuit)
        {
            Debug.Log("Quit Game now!!! Dumbass");
            Application.Quit();
        }
        else
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scenelToLoad);
            AudioManager.Instance.StopMusic();
            // while (!asyncLoad.isDone)
            // {
            //     yield return null;
            // }
        }
    }

    // IEnumerator SwitchScene(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //
    //     if (_isQuit)
    //     {
    //         Debug.Log("Quit");
    //         Application.Quit();
    //     }
    //     else
    //     {
    //         SceneManager.LoadScene(_scenelToLoad);
    //     }
    // }

    public void PlayButtonClicked()
    {
        _scenelToLoad = "CutSceneScene";
        PlayButtonClickedSound();
        mainMenuCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoadingScreen = true;
        StartCoroutine(SwitchScene(20.0f));
    }

    public void InstructionButtonClicked()
    {
        mainMenuVCam.SetActive(false);
        triggerSelectedSelection = true;
        PlayButtonClickedSound();
        ShowCanvasSelection.canvasToShow = canvasSelectionList[0];
    }

    public void OptionButtonClicked()
    {
        mainMenuVCam.SetActive(false);
        triggerSelectedSelection = true;
        PlayButtonClickedSound();
        ShowCanvasSelection.canvasToShow = canvasSelectionList[1];
    }

    public void CreditButtonClicked()
    {
        
    }

    public void QuitButtonClicked()
    {
        _isQuit = true;
        PlayButtonClickedSound();
        StartCoroutine(SwitchScene(5.0f));
    }
}