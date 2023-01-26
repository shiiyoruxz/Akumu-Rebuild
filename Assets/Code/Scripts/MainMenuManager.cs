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
    private string _sceneName = "MainMenuScene";
    private string _scenelToLoad;
    private string _backgroundMusic = "MenuBGM";

    public static string[] toolTipsText = { "Press [F] to turn the flashlight ON/OFF", 
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

    public List<GameObject> canvasSelectionList = new List<GameObject>();

    public static bool triggerSelectedSelection = false;
    public static bool returnMainMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("MenuBGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == _sceneName && returnMainMenu)
        {
            returnMainMenu = false;
            AudioManager.Instance.PlayMusic(_backgroundMusic);
        }
        
        if (_triggerLoadingScreen) 
        {
            loadingScreen.transform.GetChild(0).GetChild(1).gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
            SwapToolTipsText(3.0f);
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

    void SwapToolTipsText(float seconds)
    {
        _timer += Time.deltaTime;
        if (_timer >= seconds)
        {
            int randomIndex = UnityEngine.Random.Range(0, toolTipsText.Length);
            targetText.text = toolTipsText[randomIndex];

            _timer = 0;
        }
    }

    IEnumerator SwitchScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scenelToLoad);
        CutsceneManager.playInGameBGM = true;
    }

    public void PlayButtonClicked()
    {
        _scenelToLoad = "CutSceneScene";
        mainMenuCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoadingScreen = true;
        //FadeInBtn.showMenuBtn = true;
        StartCoroutine(SwitchScene(15.0f));
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scenelToLoad);
    }

    public void InstructionButtonClicked()
    {
        //mainMenuVCam.SetActive(false);
        triggerSelectedSelection = true;
        ShowCanvasSelection.canvasToShow = canvasSelectionList[0];
    }

    public void OptionButtonClicked()
    {
        //mainMenuVCam.SetActive(false);
        triggerSelectedSelection = true;
        ShowCanvasSelection.canvasToShow = canvasSelectionList[1];
    }

    public void CreditButtonClicked()
    {
        _scenelToLoad = "CreditScene";
        mainMenuCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoadingScreen = true;
        StartCoroutine(SwitchScene(3.0f));
    }

    public void QuitButtonClicked()
    {
        _isQuit = true;
        Application.Quit();
    }

    public static void DestroyOnReturnMenu()
    {
        Destroy(GameObject.Find("NoDestroyObject"));
    }
}