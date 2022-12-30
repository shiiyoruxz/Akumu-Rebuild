using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class MainMenuManager : MonoBehaviour
{
    private bool _isQuit = false;
    private bool _triggerLoad = false;
    private float _timer = 0;
    private string _scenelToLoad;
    private string _loadingPanelUI;
    private string _loadingIcon;
    private string[] _toolTipsText = { "Press [F] to turn the flashlight ON/OFF", 
                                        "Press [ESC] to open in-game menu or pause the game",
                                        "Use [W], [A], [S], [D] to move your character around",
                                        "Press [E] to interact with objects",
                                        "Press [R] to reload battery to the flashlight",
                                        "Press [Shift] to sprint your character" };

    public TextMeshProUGUI targetText;
    public AudioClip buttonClickedSound;
    public GameObject loadingScreen;
    public PlayableDirector director;
    public static bool instrucPressed = false;
    public GameObject mainMenuCanvas;
    public GameObject mainMenuVCam;

    // Start is called before the first frame update
    void Start()
    {
        _loadingPanelUI = "LoadingPanelUI";
        _loadingIcon = "LoadingIcon";
    }

    // Update is called once per frame
    void Update()
    {
        if (_triggerLoad) 
        {
            loadingScreen.transform.Find(_loadingPanelUI).Find(_loadingIcon).gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
            SwapToolTipsText(5.0f);
        }

        //Debug.Log("Instruction pressed?: " + instrucPressed);
        
        if (instrucPressed && mainMenuCanvas.activeSelf == false)
        {
            director.Resume();

            if (director.initialTime == 5.0f)
            {
                director.initialTime = 0.0f;
                //mainMenuVCam.SetActive(true);
                Debug.Log("YesYesYes");
                //director.Evaluate();
                director.Play();
                //director.Resume();
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
        _scenelToLoad = "SampleScene";
        PlayButtonClickedSound();
        mainMenuCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoad = true;
        StartCoroutine(SwitchScene(20.0f));
    }

    public void InstructionButtonClicked()
    {
        mainMenuVCam.SetActive(false);
        instrucPressed = true;
        //_scenelToLoad = "InstructionsScene";
        PlayButtonClickedSound();
        //mainMenuScreen.SetActive(false);
        //loadingScreen.SetActive(true);
        //_triggerLoad = true;
        //StartCoroutine(SwitchScene(5.0f));
        // if (mainMenuCanvas.activeSelf == false)
        //     director.Resume();
    }

    public void QuitButtonClicked()
    {
        _isQuit = true;
        PlayButtonClickedSound();
        StartCoroutine(SwitchScene(5.0f));
    }
}