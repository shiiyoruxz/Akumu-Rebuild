using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInBtn : MonoBehaviour
{
    public float fadeDuration;
    public GameObject panelUIObject;
    public List<GameObject> btnObjList = new List<GameObject>();
    private List<CanvasGroup> _btnCanvasGroupList = new List<CanvasGroup>();
    private bool _isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panelUIObject.activeSelf && _isTrigger == false)
        {
            for (int i = 0; i < btnObjList.Count; i++)
            {
                _btnCanvasGroupList.Add(btnObjList[i].GetComponent<CanvasGroup>());
            }
            
            _isTrigger = true;
            StartCoroutine(TriggerFade());
        }
    }
    
    IEnumerator TriggerFade()
    {
        for (int i = 0; i < _btnCanvasGroupList.Count; i++)
        {
            StartCoroutine(FadeIn(_btnCanvasGroupList[i], fadeDuration));
            yield return new WaitForSeconds(0.35f);
        }
    }

    IEnumerator FadeIn(CanvasGroup showBtn, float fadeDuration)
    {
        float counter = 0.0f;
        while (counter < fadeDuration)
        { 
            counter += Time.deltaTime;
            showBtn.alpha = Mathf.Lerp(0, 1.0f, counter / fadeDuration);
            yield return null;
        }
    }
}
