using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MainMenuBGM : MonoBehaviour
{
    public AudioClip mainMenuBGM;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = mainMenuBGM;
        audio.Play();

        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}