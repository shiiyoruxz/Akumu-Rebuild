using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class AIDoorOpen : MonoBehaviour
{
    public float doorOpenTime = 3.0f;
    public List<GameObject> doorType = new List<GameObject>();
    
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
    private GameObject currentDoor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;

        // while raycast hit the collider of the door
        if(Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
        {
            if (hit.collider.gameObject.tag == "ClassroomDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "ToiletDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "InfirmaryTwoDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "InfirmaryOneDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "GymOnePushDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "LibraryDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "ToiletInDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                OpenDoor(currentDoor);
            }
            if (hit.collider.gameObject.tag == "LabDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                for (int i = 1; i < 3; i++)
                {
                    GameObject door = doorType[i];
                    doorIsOpen = true; 
                    door.GetComponent<Animator>().SetBool("Trigger", true);
                }
            }
            if (hit.collider.gameObject.tag == "LabDoor2" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                for (int i = 4; i < 6; i++)
                {
                    GameObject door = doorType[i];
                    doorIsOpen = true; 
                    door.GetComponent<Animator>().SetBool("Trigger", true);
                }
            }
            if (hit.collider.gameObject.tag == "GymSD1" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                for (int i = 8; i < 10; i++)
                {
                    GameObject door = doorType[i];
                    doorIsOpen = true; 
                    door.GetComponent<Animator>().SetBool("Trigger", true);
                }
            }
            if (hit.collider.gameObject.tag == "GymSD2" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                for (int i = 10; i < 12; i++)
                {
                    GameObject door = doorType[i];
                    doorIsOpen = true; 
                    door.GetComponent<Animator>().SetBool("Trigger", true);
                }
            }
            if (hit.collider.gameObject.tag == "GymSD3" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                for (int i = 12; i < 14; i++)
                {
                    GameObject door = doorType[i];
                    doorIsOpen = true; 
                    door.GetComponent<Animator>().SetBool("Trigger", true);
                }
            }
        }
        
        // detect the door status (shut / open)
        if(doorIsOpen)
        {
            doorTimer += Time.deltaTime;
            if(doorTimer > doorOpenTime)
            {
                if (currentDoor.tag == "LabDoor")
                {
                    for (int i = 1; i < 3; i++)
                    {
                        GameObject currentDoor = doorType[i];
                        doorIsOpen = false;
                        currentDoor.GetComponent<Animator>().SetBool("Trigger", false);
                    }
                    doorTimer = 0;
                }
                else if (currentDoor.tag == "LabDoor2")
                {
                    for (int i = 4; i < 6; i++)
                    {
                        GameObject currentDoor = doorType[i];
                        doorIsOpen = false;
                        currentDoor.GetComponent<Animator>().SetBool("Trigger", false);
                    }
                    doorTimer = 0;
                }
                else if (currentDoor.tag == "GymSD1")
                {
                    for (int i = 8; i < 10; i++)
                    {
                        GameObject currentDoor = doorType[i];
                        doorIsOpen = false;
                        currentDoor.GetComponent<Animator>().SetBool("Trigger", false);
                    }
                    doorTimer = 0;
                }
                else if (currentDoor.tag == "GymSD2")
                {
                    for (int i = 10; i < 12; i++)
                    {
                        GameObject currentDoor = doorType[i];
                        doorIsOpen = false;
                        currentDoor.GetComponent<Animator>().SetBool("Trigger", false);
                    }
                    doorTimer = 0;
                }
                else if (currentDoor.tag == "GymSD3")
                {
                    for (int i = 12; i < 14; i++)
                    {
                        GameObject currentDoor = doorType[i];
                        doorIsOpen = false;
                        currentDoor.GetComponent<Animator>().SetBool("Trigger", false);
                    }
                    doorTimer = 0;
                }
                else
                {
                    ShutDoor(currentDoor);
                    doorTimer = 0.0f;
                }
            }
        }
    }
    
    void OpenDoor(GameObject door)
    {
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.clip = openDoorSound[];
        // audio.Play();
        doorIsOpen = true; 
        door.GetComponent<Animator>().SetBool("Trigger", true);
    }
    
    void ShutDoor(GameObject door)
    {
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.clip = shutDoorSound[];
        // audio.Play();
        doorIsOpen = false;
        door.GetComponent<Animator>().SetBool("Trigger", false);
    }
}

