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
    public List<GameObject> passwordButtons;

    private string _passwordPanelName = "DigitalPasswordLockPanel";
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
                PlayerInteraction.digitalLockIsOpen = false;
                GameObject.Find("PasswordLock").SetActive(false);
                GameObject.Find("PasswordLock2").SetActive(false);
                GameObject.Find(_passwordPanelName).SetActive(false);
                
                // Make the player unable to interact with the numpad anymore
                for (int i = 0; i < passwordButtons.Count; i++)
                {
                    passwordButtons[i].GetComponent<Button>().interactable = false;
                }
                
                GameObject.Find("DigitalPasswordLockUI").GetComponent<Canvas>().sortingOrder = 0;
                Cursor.lockState = CursorLockMode.Locked;
                firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                
                ObjectivesSystem.Instance.objectiveText.text = ObjectivesSystem.Instance.objectivesDescriptions[5];
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
