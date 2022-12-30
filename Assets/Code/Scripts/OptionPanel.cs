using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public GameObject optionPanelUI;
    private List<GameObject> _toggleList = new List<GameObject>();
    private List<GameObject> _sliderList = new List<GameObject>();

    private bool _initObjList = true;
    private string[] tagName = {"OptionToggle", "OptionSlider"};
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (optionPanelUI.activeSelf && _initObjList)
        {
            _initObjList = false;
            foreach (Transform child in optionPanelUI.transform)
            {
                if (child.gameObject.CompareTag(tagName[0]))
                {
                    _toggleList.Add(child.gameObject);
                }
                else if (child.gameObject.CompareTag(tagName[1]))
                {
                    _sliderList.Add(child.gameObject);
                }
            }
        }

        for (int i = 0; i < _toggleList.Count; i++)
        {
            if (_toggleList[i].GetComponent<Toggle>().isOn)
            {
                _sliderList[i].GetComponent<Slider>().interactable = true;
                _sliderList[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                _sliderList[i].transform.GetChild(2).GetChild(0).GetComponent<Image>().color =
                    new Color32(255, 255, 255, 255);
                
            }
            else
            {
                _sliderList[i].GetComponent<Slider>().interactable = false;
                _sliderList[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                _sliderList[i].transform.GetChild(2).GetChild(0).GetComponent<Image>().color =
                    new Color32(100, 100, 100, 255);
            }
        }
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_sliderList[0].GetComponent<Slider>().value);
    }
    
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sliderList[1].GetComponent<Slider>().value);
    }
}
