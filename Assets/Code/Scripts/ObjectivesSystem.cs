using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesSystem : MonoBehaviour
{
    public static ObjectivesSystem Instance;

    public GameObject ObjectivePanelUI;
    public TextMeshProUGUI objectiveText;
    public string[] objectivesDescriptions;

    private bool _inactive = true;

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

    // Start is called before the first frame update
    void Start()
    {
        objectiveText.text = objectivesDescriptions[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionalObjectivesActiveStatus()
    {
        ObjectivePanelUI.transform.GetChild(2).gameObject.SetActive(_inactive);
        ObjectivePanelUI.transform.GetChild(3).gameObject.SetActive(_inactive);
        _inactive = !_inactive;
    }
}
