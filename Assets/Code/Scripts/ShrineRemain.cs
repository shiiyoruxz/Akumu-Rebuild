using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineRemain : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(MainManager.Instance.gameObject.name);
        player = MainManager.Instance.gameObject.transform.GetChild(0).gameObject;
        player.transform.position = new Vector3(-3.834f, 2.467f, 11.25f);
        player.transform.rotation = Quaternion.Euler(0.0f, 174.4f, 0.0f);
        player.transform.localScale = new Vector3(3.2f, 3.2f, 3.2f);
        player.GetComponent<FirstPersonController>().walkSpeed = 4.0f;
        player.GetComponent<FirstPersonController>().sprintSpeed = 6.0f;
        player.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialEnterShrine").gameObject.SetActive(true);
        
    }

    private void Update()
    {
        if (!player.activeSelf)
        {
            player.SetActive(true);
        }
    }

}
