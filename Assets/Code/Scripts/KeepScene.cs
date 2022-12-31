using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScene : MonoBehaviour
{

    public static KeepScene instance;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
     
    }

    

}
