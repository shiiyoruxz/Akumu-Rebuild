using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExtensionObject : MonoBehaviour
{
    public static ExtensionObject Instance => _instance;
    private static ExtensionObject _instance;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        GameObject extensionObject = new GameObject("ExtensionObject");
        extensionObject.AddComponent<ExtensionObject>();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }
}
