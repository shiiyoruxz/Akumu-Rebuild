using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressKeyContinue : MonoBehaviour
{
    public float speedFade;
    public float timeLapsed;
    public TextMeshProUGUI pressKeyText;
    private float _alpha;

    private bool _test = false;
    private bool _keyIsPressed = false;
    public PlayableDirector director;

    public GameObject titleImageObject;
    public GameObject textObject;
    public GameObject panelUIObject;

    public static bool returnMainMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _alpha += speedFade * Time.deltaTime;
        pressKeyText.color = new Color(pressKeyText.color.r, pressKeyText.color.g, pressKeyText.color.b,
            Mathf.PingPong(_alpha, timeLapsed));

        if (Input.anyKey && !_keyIsPressed)
        {
            _keyIsPressed = true;
            director.Play();

            titleImageObject.SetActive(false);
            textObject.SetActive(false);
        }

        if (director.time >= 5.0f && panelUIObject.activeSelf == false && _test == false)
        {
            if (_test == false)
            {
                _test = true;
                director.Pause();
            }
            // else
            // {
            //     Debug.Log("Test here works");
            //     // director.Play();
            //     // director.Evaluate();
            // }
            // //director.initialTime = 5.0f;
            panelUIObject.SetActive(true);
        }
        //Debug.Log("Time" + director.time);
    }
}