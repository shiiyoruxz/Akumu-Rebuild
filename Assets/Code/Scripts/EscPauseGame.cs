using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EscPauseGame : MonoBehaviour
{
    public List<GameObject> ESCPanelList = new List<GameObject>();
    public GameObject firstPersonController;
    public TextMeshProUGUI targetText;
    public static bool gameIsPaused = false;
    
    public List<GameObject> UIToDisable = new List<GameObject>();

    private string _mainMenu = "MainMenuScene";
    private float _timer = 0;
    private bool _triggerLoading = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerInteraction.digitalLockIsOpen == false && PlayerInteraction.diaryIsOpen == false && PlayerInteraction.ghostBookIsOpen == false && AIController.playerIsDead == false)
        {
            // Debug.Log("Esc Pressed");
            gameIsPaused = !gameIsPaused;
            // Debug.Log(gameIsPaused);
            AudioManager.Instance.PauseMusic();
            PauseGame();
        }

        if (ESCPanelList[2].GetComponent<CanvasGroup>().alpha == 1) 
        {
            ESCPanelList[2].transform.GetChild(1).gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
            SwapToolTipsText(5.0f);
        }
    }

    void PauseGame()
    {
        if (gameIsPaused && _triggerLoading == false)
        {
            for (int i = 0; i < UIToDisable.Count; i++)
            {
                UIToDisable[i].SetActive(false);
            }
            
            Time.timeScale = 0.0f;
            ESCPanelList[0].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 3;
            ESCPanelList[0].GetComponentInParent<Volume>().enabled = true;
            firstPersonController.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            ESCPanelList[0].GetComponent<CanvasGroup>().alpha = 1;
            ESCPanelList[0].GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        for (int i = 0; i < UIToDisable.Count; i++)
        {
            UIToDisable[i].SetActive(true);
        }
        
        AudioManager.Instance.ResumeMusic();
        Time.timeScale = 1.0f;
        ESCPanelList[0].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        ESCPanelList[0].GetComponentInParent<Volume>().enabled = false;
        firstPersonController.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < ESCPanelList.Count; i++)
        {
            ESCPanelList[i].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
            ESCPanelList[i].GetComponent<CanvasGroup>().alpha = 0;
            ESCPanelList[i].GetComponent<CanvasGroup>().interactable = false;
        }
    }

    IEnumerator SwitchScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _triggerLoading = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_mainMenu);
        
        ESCPanelList[2].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        ESCPanelList[2].GetComponent<CanvasGroup>().alpha = 0;
        MainMenuManager.DestroyOnReturnMenu();
        gameIsPaused = false;
        //ESCCanvasList[2].SetActive(false);
        //ESCCanvasList[0].SetActive(true);
        //AudioManager.Instance.StopMusic();
    }
    
    void SwapToolTipsText(float seconds)
    {
        _timer += Time.deltaTime;
        if (_timer >= seconds)
        {
            int randomIndex = UnityEngine.Random.Range(0, MainMenuManager.toolTipsText.Length);
            targetText.text = MainMenuManager.toolTipsText[randomIndex];

            _timer = 0;
        }
    }

    public void ResumeButtonClicked()
    {
        // Debug.Log("Resume Pressed");
        gameIsPaused = !gameIsPaused;
        ResumeGame();
    }
    
    public void OptionButtonCliked()
    {
        AudioManager.Instance.ResumeMusic();
        
        ESCPanelList[0].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        //ESCCanvasList[0].SetActive(false);
        ESCPanelList[0].GetComponent<CanvasGroup>().alpha = 0;
        ESCPanelList[0].GetComponent<CanvasGroup>().interactable = false;
        
        ESCPanelList[1].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 5;
        //ESCCanvasList[1].SetActive(true);
        ESCPanelList[1].GetComponent<CanvasGroup>().alpha = 1;
        ESCPanelList[1].GetComponent<CanvasGroup>().interactable = true;
    }

    public void BackButtonClicked()
    {
        AudioManager.Instance.PauseMusic();
        
        ESCPanelList[1].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        //ESCCanvasList[1].SetActive(false);
        ESCPanelList[1].GetComponent<CanvasGroup>().alpha = 0;
        ESCPanelList[1].GetComponent<CanvasGroup>().interactable = false;
        
        ESCPanelList[0].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 3;
        //ESCCanvasList[0].SetActive(true);
        ESCPanelList[0].GetComponent<CanvasGroup>().alpha = 1;
        ESCPanelList[0].GetComponent<CanvasGroup>().interactable = true;;
    }

    public void ReturnMenuButtonClicked()
    {
        MainMenuManager.returnMainMenu = true;
        FlashlightManager.firstBattery = true;
        PlayerInteraction.surpriseToiletVent = true;
        Inventory.numBattery = 0;
        AudioManager.Instance.ResumeMusic();
        CutsceneManager.currentCutscene = 0;
        
        Time.timeScale = 1.0f;
        _triggerLoading = true;
        FadeInBtn.showMenuBtn = true;
        
        ESCPanelList[0].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 0;
        //ESCCanvasList[0].SetActive(false);
        ESCPanelList[0].GetComponent<CanvasGroup>().alpha = 0;
        ESCPanelList[0].GetComponent<CanvasGroup>().interactable = false;
        
        ESCPanelList[2].transform.parent.gameObject.GetComponent<Canvas>().sortingOrder = 5;
        //ESCCanvasList[2].SetActive(true);
        ESCPanelList[2].GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(SwitchScene(3.0f));
    }
}
