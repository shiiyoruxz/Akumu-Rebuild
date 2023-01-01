using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float pickUpDistance = 2.0f;

    public float doorOpenTime = 3.0f;
    public List<GameObject> twoDoorType = new List<GameObject>();

    public static string[] doorTags =
    {
        "ClassroomDoor", "ToiletDoor", "InfirmaryTwoDoor", "InfirmaryOneDoor", "GymOnePushDoor", "LibraryDoor",
        "ToiletInDoor", "MusicDoor", "JumpScareDoor"
    };
    public GameObject digitalPasswordUI;
    public GameObject firstPersonController;
    
    private bool doorIsOpen = true;
    private GameObject currentDoor;
    private GameObject currentObject;
    private GameObject playerCheckPoint;
    private GameObject effect;
    
    int startIndex = 0;
    int endIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        digitalPasswordUI = GameObject.Find("DigitalPasswordLockPanel");
        digitalPasswordUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DoorInteraction();
        MapInteraction();
    }
    
    void MapInteraction()
    {
        RaycastHit hit;

        // Map Interaction
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out hit, pickUpDistance))
        {
            // Check if the hit object has the "PickUp" tag
            if (hit.collider.tag == "Collectible")
            {
                // Pick up the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp();
                }
            }
            if (hit.collider.tag == "Book")
            {
                // Trigger Book
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Read();
                }
            }
            if (hit.collider.tag == "ToiletVent")
            {
                // Trigger Vent
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentObject = hit.collider.gameObject;
                    ToiletEventTrigger(currentObject);
                }
            }
            if (hit.collider.tag == "DigitalPasswordLock")
            {
                // Trigger Digital Password Lock
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UnlockInfirmaryDoor();
                }

                if (digitalPasswordUI.activeSelf && Input.GetKeyDown(KeyCode.C))
                {
                    GameObject.Find("DigitalPasswordLockPanel").SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                }
            }
        }
    }

    void DoorInteraction()
    {
        RaycastHit hit;

        // Check for "E" key being released
        if (Input.GetKeyUp(KeyCode.E))
        {
            // Check for a door in front of the player
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3.5f))
            {
                // Check for a door with a matching tag
                foreach (string doorTag in doorTags)
                {
                    if (hit.collider.gameObject.tag == doorTag)
                    {
                        currentDoor = hit.collider.gameObject;

                        // Open or close the door based on its current state
                        if (doorIsOpen)
                        {
                            ShutDoor(currentDoor);
                        }
                        else
                        {
                            OpenDoor(currentDoor);
                        }
                    }
                }
                // Check for a multi-door with a matching tag
                if (hit.collider.gameObject.tag == "LabDoor")
                {
                    currentDoor = hit.collider.gameObject;
                    startIndex = 0;
                    endIndex = 2;
                    OpenCloseMultiDoor(startIndex, endIndex);
                }
                else if (hit.collider.gameObject.tag == "LabDoor2")
                {
                    currentDoor = hit.collider.gameObject;
                    startIndex = 2;
                    endIndex = 4;
                    OpenCloseMultiDoor(startIndex, endIndex);
                }
                else if (hit.collider.gameObject.tag == "GymSD1")
                {
                    currentDoor = hit.collider.gameObject;
                    startIndex = 4;
                    endIndex = 8;
                    OpenCloseMultiDoor(startIndex, endIndex);
                }
                else if (hit.collider.gameObject.tag == "GymSD2")
                {
                    currentDoor = hit.collider.gameObject;
                    startIndex = 6;
                    endIndex = 8;
                    OpenCloseMultiDoor(startIndex, endIndex);
                }
                else if (hit.collider.gameObject.tag == "GymSD3")
                {
                    currentDoor = hit.collider.gameObject;
                    startIndex = 8;
                    endIndex = 10;
                    OpenCloseMultiDoor(startIndex, endIndex);
                }
                else if (hit.collider.gameObject.tag == "LockedDoor")
                {
                    DoorLockedDialogue();
                }
            }
        }
    }
    
    // Item Pickup
    void PickUp()
    {
        // Add the item to the player's inventory or perform some other action

        Debug.Log("Item Nearby!");
    }

    // Trigger Book
    void Read()
    {
        // Open the book

        Debug.Log("Read Book");
    }
    
    // Door Open
    void OpenDoor(GameObject door)
    {
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.clip = openDoorSound[];
        // audio.Play();
        doorIsOpen = true;
        door.GetComponent<Animator>().SetBool("Trigger", true);
    }
    
    // Door Close
    void ShutDoor(GameObject door)
    {
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.clip = shutDoorSound[];
        // audio.Play();
        doorIsOpen = false;
        door.GetComponent<Animator>().SetBool("Trigger", false);
    }
    
    // Close and Open for Two Door
    void OpenCloseMultiDoor(int startIndex, int endIndex)
    {
        // Toggle doorIsOpen flag
        doorIsOpen = !doorIsOpen;

        // Open or close doors
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject door = twoDoorType[i];
            Animator animator = door.GetComponent<Animator>();
            animator.SetBool("Trigger", doorIsOpen);
        }
    }

    // Trigger LockedDoor
    void DoorLockedDialogue()
    {
        Debug.Log("This Door Is Locked!");
    }
    
    IEnumerator playerTeleportToLab()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2.5f);

        // Do something after 3 seconds
        // Change player checkpoint
        playerCheckPoint = GameObject.FindWithTag("Player");
        playerCheckPoint.transform.localPosition = new Vector3(12.054f, 0.547f, -23.089f);
        playerCheckPoint.transform.localRotation = Quaternion.Euler(0f, 357.9f, 0f);
        
        currentDoor = GameObject.Find("LockedLabDoor");
        Debug.Log(currentDoor);
        currentDoor.SetActive(false);
        effect = GameObject.Find("DarkenScreen");
        effect.GetComponent<Animator>().SetBool("Trigger", false);
    }
    
    // Toilet Event
    void ToiletEventTrigger(GameObject currentObject)
    {
        // Vent animate
        currentObject.transform.localPosition = new Vector3(2.851f, 0.06018281f, 10.52258f);
        currentObject.transform.localRotation = Quaternion.Euler(-90f, 112.884f, 0f);
       
        effect = GameObject.Find("DarkenScreen");
        effect.GetComponent<Animator>().SetBool("Trigger", true);

        StartCoroutine(playerTeleportToLab());
    }

    // Trigger Infirmary Digital Lock to unlock the Door
    void UnlockInfirmaryDoor()
    {
        digitalPasswordUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        firstPersonController.GetComponent<FirstPersonController>().enabled = false;
    }
}
