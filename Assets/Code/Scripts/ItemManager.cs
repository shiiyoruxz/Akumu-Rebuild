using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject torchLight;
    
    // Start is called before the first frame update
    void Start()
    {
        torchLight = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (CutsceneLoader.num > 1)
        {
            torchLight.SetActive(true);
        }
        else
        {
            torchLight.SetActive(false);
        }
    }
}
