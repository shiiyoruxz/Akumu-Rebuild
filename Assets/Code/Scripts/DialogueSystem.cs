using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text textComponent;
    public string[] lines;
    public float nextDialogueSpeed = 4.5f;
 
    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(nextDialogue());

    }

    void startDialogue()
    {
        index = 0;
        textComponent.text = lines[index];
    }
    

    void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator nextDialogue()
    {
        yield return new WaitForSeconds(nextDialogueSpeed);
        if (textComponent.text == lines[index])
        {
            nextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
    
}
