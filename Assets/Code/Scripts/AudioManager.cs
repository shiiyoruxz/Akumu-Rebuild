using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sFXSounds;
    public AudioSource musicSource, sFXSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainMenuBGM");
    }

    public void PlayMusic(string name)
    {
        Sound audio = Array.Find(musicSounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found!");
        }
        else
        {
            musicSource.clip = audio.clip;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(string name)
    {
        Sound audio = Array.Find(sFXSounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.Log("Sound: " + name + " not found!");
        }
        else
        {
            sFXSource.PlayOneShot(audio.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    
    public void ToggleSFX()
    {
        sFXSource.mute = !sFXSource.mute;
    }
    
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    public void SFXVolume(float volume)
    {
        sFXSource.volume = volume;
    }
}
