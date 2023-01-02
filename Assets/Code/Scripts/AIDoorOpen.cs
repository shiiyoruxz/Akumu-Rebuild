using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDoorOpen : MonoBehaviour
{
    public float doorOpenTime = 3.0f;
    public List<GameObject> twoDoorType = new List<GameObject>();

    public static string[] doorTags = { "ClassroomDoor", "ToiletDoor", "InfirmaryTwoDoor", "InfirmaryOneDoor", "GymOnePushDoor", "LibraryDoor", "ToiletInDoor" };
    
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
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
    }

    void DoorInteraction()
    {
        RaycastHit hit;

        // while raycast hit the collider of the door
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3.5f))
        {
            foreach (string doorTag in doorTags)
            {
                if (hit.collider.gameObject.tag == doorTag && doorIsOpen == false)
                {
                    currentDoor = hit.collider.gameObject;
                    OpenDoor(currentDoor);
                }
            }

            if (hit.collider.gameObject.tag == "LabDoor" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                startIndex = 0;
                endIndex = 2;
                OpenMultiDoor(startIndex, endIndex);
            }

            if (hit.collider.gameObject.tag == "LabDoor2" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                startIndex = 2;
                endIndex = 4;
                OpenMultiDoor(startIndex, endIndex);
            }

            if (hit.collider.gameObject.tag == "GymSD1" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                startIndex = 4;
                endIndex = 6;
                OpenMultiDoor(startIndex, endIndex);
            }

            if (hit.collider.gameObject.tag == "GymSD2" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                startIndex = 6;
                endIndex = 8;
                OpenMultiDoor(startIndex, endIndex);
            }

            if (hit.collider.gameObject.tag == "GymSD3" && doorIsOpen == false)
            {
                currentDoor = hit.collider.gameObject;
                startIndex = 8;
                endIndex = 10;
                OpenMultiDoor(startIndex, endIndex);
            }
        }

        // detect the door status (shut / open)if (doorIsOpen)
        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > doorOpenTime)
            {
                if (currentDoor.tag == "LabDoor")
                {
                    startIndex = 0;
                    endIndex = 2;
                    ShutMultiDoor(startIndex, endIndex);
                }
                else if (currentDoor.tag == "LabDoor2")
                {
                    startIndex = 2;
                    endIndex = 4;
                    ShutMultiDoor(startIndex, endIndex);
                }
                else if (currentDoor.tag == "GymSD1")
                {
                    startIndex = 4;
                    endIndex = 6;
                    ShutMultiDoor(startIndex, endIndex);
                }
                else if (currentDoor.tag == "GymSD2")
                {
                    startIndex = 6;
                    endIndex = 8;
                    ShutMultiDoor(startIndex, endIndex);
                }
                else if (currentDoor.tag == "GymSD3")
                {
                    startIndex = 8;
                    endIndex = 10;
                    ShutMultiDoor(startIndex, endIndex);
                }
                else
                {
                    ShutDoor(currentDoor);
                    doorTimer = 0.0f;
                }
            }
        }
    }

    void OpenMultiDoor(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject door = twoDoorType[i];
            OpenDoor(door);
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
    
    void ShutMultiDoor(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject door = twoDoorType[i];
            ShutDoor(door);
        }
        doorTimer = 0;
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

