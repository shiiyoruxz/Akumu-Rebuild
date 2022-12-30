using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float pickUpDistance = 2.0f;

    public float doorOpenTime = 3.0f;
    public List<GameObject> twoDoorType = new List<GameObject>();

    public static string[] doorTags =
    {
        "ClassroomDoor", "ToiletDoor", "InfirmaryTwoDoor", "InfirmaryOneDoor", "GymOnePushDoor", "LibraryDoor",
        "ToiletInDoor"
    };

    private bool doorIsOpen = true;
    private GameObject currentDoor;
    int startIndex = 0;
    int endIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DoorInteraction();
        ItemInteract();
    }

    void ItemInteract()
    {
        RaycastHit hit;

        // Item Pickup
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
                // Pick up the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Read();
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
            }
        }
    }
    
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
}
