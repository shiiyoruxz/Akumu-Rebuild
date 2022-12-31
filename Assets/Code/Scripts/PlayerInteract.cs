using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Animator Door = null;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
        {
            
            if (hit.collider.gameObject.CompareTag("door"))
            {
                Debug.Log("door");

            }
        }
        Debug.DrawRay(transform.position, transform.forward, Color.green, 100);
    }
    
}
