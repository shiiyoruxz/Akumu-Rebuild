using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButtonOnClick : MonoBehaviour
{
    public string password;
    public string correctPassword;
    public Text screenText;
    public Text result;

    public GameObject firstPersonController;
    
    void Start()
    {
        
    }

    private void Update()
    {
        showText();
    }

    public void EnterPassword(string digit)
    {
        AudioManager.Instance.PlaySFX("PasswordPressed");
        password += digit;
        checkPassword();
    }

    void checkPassword()
    {
        if (password.Length == 4)
        {
            if (password == correctPassword)
            {
                AudioManager.Instance.PlaySFX("PasswordCorrect");
                password = "";
                password = "UNLOCKED";
                GameObject.Find("PasswordLock").SetActive(false);
                GameObject.Find("PasswordLock2").SetActive(false);
                GameObject.Find("DigitalPasswordLockPanel").SetActive(false);
                GameObject.Find("DigitalPasswordLockUI").GetComponent<Canvas>().sortingOrder = 0;
                Cursor.lockState = CursorLockMode.Locked;
                firstPersonController.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                AudioManager.Instance.PlaySFX("PasswordIncorrect");
                password = "";
            }
        }
    }

    public void showText()
    {
        screenText.text = password;
    }
}
