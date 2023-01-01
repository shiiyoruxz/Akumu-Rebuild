using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FadeInBtn : MonoBehaviour
{
    public float fadeDuration;
    public GameObject panelUIObject;
    public List<GameObject> btnObjList = new List<GameObject>();
    public static bool showMenuBtn = true;
    public static bool finishHide = false;
    
    private List<CanvasGroup> _btnCanvasGroupList = new List<CanvasGroup>();
    private bool _initBtnCanvasGroup = true;
    private bool _triggInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panelUIObject.activeSelf && _initBtnCanvasGroup)
        {
            for (int i = 0; i < btnObjList.Count; i++)
            {
                _btnCanvasGroupList.Add(btnObjList[i].GetComponent<CanvasGroup>());
            }
            
            _initBtnCanvasGroup = false;
            
            // showMenuBtn = false;
            // StartCoroutine(TriggerFade(true));
        }

        if (panelUIObject.activeSelf && showMenuBtn)
        {
            StartCoroutine(TriggerFade(true));
            showMenuBtn = false;
            _triggInteractable = true;
        }

        if (panelUIObject.activeSelf && _btnCanvasGroupList[0].alpha == 1 && _btnCanvasGroupList[4].alpha == 1 && _triggInteractable)
        {
            for (int i = 0; i < btnObjList.Count; i++)
            {
                btnObjList[i].GetComponent<Button>().interactable = true;
            }
        }

        if (MainMenuManager.triggerSelectedSelection && finishHide == false)
        {
            //MainMenuManager.instrucPressed = false;
            finishHide = true;
            _triggInteractable = false;
            StartCoroutine(TriggerFade(false));
            
            for (int i = 0; i < btnObjList.Count; i++)
            {
                btnObjList[i].GetComponent<Button>().interactable = false;
            }
        }

        if (panelUIObject.activeSelf && showMenuBtn == false && finishHide)
        {
            if (_btnCanvasGroupList[0].alpha == 0 && _btnCanvasGroupList[4].alpha == 0)
            {
                panelUIObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    
    IEnumerator TriggerFade(bool fadeIn)
    {
        for (int i = 0; i < _btnCanvasGroupList.Count; i++)
        {
            StartCoroutine(FadeEffects(_btnCanvasGroupList[i], fadeDuration, fadeIn));
            yield return new WaitForSeconds(0.35f);
        }
    }

    IEnumerator FadeEffects(CanvasGroup showBtn, float fadeDuration, bool fadeIn)
    {
        float counter = 0.0f;
        if (fadeIn)
        {
            while (counter < fadeDuration)
            { 
                counter += Time.deltaTime;
                showBtn.alpha = Mathf.Lerp(0, 1.0f, counter / fadeDuration);
                yield return null;
            }
        }
        else
        { 
            while (counter < fadeDuration)
            { 
                counter += Time.deltaTime;
                showBtn.alpha = Mathf.Lerp(1.0f, 0, counter / fadeDuration); 
                yield return null;
            }
        }
    }
}
