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
        Debug.Log("Pressed");
        password += digit;
        checkPassword();
    }

    void checkPassword()
    {
        if (password.Length == 4)
        {
            if (password == correctPassword)
            {
                password = "";
                password = "UNLOCKED";
                GameObject.Find("PasswordLock").SetActive(false);
                GameObject.Find("DigitalPasswordLockPanel").SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                firstPersonController.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                password = "";
            }
        }
    }

    public void showText()
    {
        screenText.text = password;
    }
}
