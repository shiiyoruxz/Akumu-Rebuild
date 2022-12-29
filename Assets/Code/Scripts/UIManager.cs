using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class UIManager : MonoBehaviour
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
    public GameObject mainMenuScreen;
    public GameObject loadingScreen;

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
        if (_isQuit)
        {
            Debug.Log("Quit Game now!!! Dumbass");
            Application.Quit();
        }
        else
        {
            StartCoroutine(WaitDuration(5.0f));
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scenelToLoad);   
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            StartCoroutine(WaitDuration(5.0f));
        }
    }

    IEnumerator WaitDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
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
        mainMenuScreen.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoad = true;
        StartCoroutine(SwitchScene(20.0f));
    }

    public void InstructionButtonClicked()
    {
        _scenelToLoad = "InstructionsScene";
        PlayButtonClickedSound();
        mainMenuScreen.SetActive(false);
        loadingScreen.SetActive(true);
        _triggerLoad = true;
        StartCoroutine(SwitchScene(5.0f));
    }

    public void QuitButtonClicked()
    {
        _isQuit = true;
        PlayButtonClickedSound();
        StartCoroutine(SwitchScene(5.0f));
    }
}