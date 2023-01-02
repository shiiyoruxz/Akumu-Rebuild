using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float pickUpDistance = 0.5f;

    public float doorOpenTime = 3.0f;
    public List<GameObject> twoDoorType = new List<GameObject>();

    public static string[] doorTags =
    {
        "ClassroomDoor", "ToiletDoor", "InfirmaryTwoDoor", "InfirmaryOneDoor", "GymOnePushDoor", "LibraryDoor",
        "ToiletInDoor", "MusicDoor", "JumpScareDoor"
    };
    public GameObject digitalPasswordUI;
    public GameObject firstPersonController;
    
    public static bool digitalLockIsOpen = false;
    public static bool hintTextShow = false;
    private bool GameIsOver = false;
    private bool doorIsOpen = false;
    private bool ghostBookIsOpen = false;
    private bool diaryIsOpen = false;
    private GameObject currentDoor;
    private GameObject currentObject;
    private GameObject playerCheckPoint;
    private GameObject effect;
    public GameObject inventory;
    private GameObject destroyDoor;
    
    int startIndex = 0;
    int endIndex = 0;
    
    private static bool isHide = false;
    private static Transform temp_position;

    // Start is called before the first frame update
    void Start()
    {
        digitalPasswordUI.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialAtTheBeginning").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        DoorInteraction();
        MapInteraction();
        
        if (EscPauseGame.gameIsPaused)
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }

        if (GameIsOver)
        {
            gameCompleted();
        }
        
        //Check num of battery && show reload text
        if (gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Light>().intensity <= 1.7 && !FlashlightManager.firstBattery && Inventory.numBattery > 0)
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "BucketFallDown")
        {
            col.gameObject.transform.parent.GetComponent<Animator>().SetBool("Trigger", true);
            gameObject.transform.parent.GetChild(0).gameObject.transform.GetChild(10).transform
                .Find("SurpriseBucket").transform.GetChild(0).gameObject.SetActive(false);
            AIController.patrolPhase = 3;
        }
    }

    void MapInteraction()
    {
        RaycastHit hit;

        // Map Interaction
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out hit, pickUpDistance))
        {
            if (hit.collider.tag != "Untagged")
            {
                //Press E to interact text
                gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            }
            
            // Check if the hit object has the "PickUp" tag
            if (hit.collider.tag == "Collectable")
            {
                // Pick up the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp(hit.collider.gameObject);
                    
                }
            }
            if (hit.collider.tag == "Book")
            {
                // Trigger Book
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.gameObject.name == "ghostBook")
                    {
                        // Open the book
                        if (ghostBookIsOpen)
                        {
                            //dialog of read ghost book
                            gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialGhostBook").gameObject.SetActive(true);
                            firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                            gameObject.transform.parent.transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(false);
                            ghostBookIsOpen = false;
                        }
                        else
                        {
                            firstPersonController.GetComponent<FirstPersonController>().enabled = false;
                            gameObject.transform.parent.transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(true);
                            ghostBookIsOpen = true;
                        }
                    }
                    
                    if (hit.collider.gameObject.name == "Diary")
                    {
                        // Open the book
                        if (diaryIsOpen)
                        {
                            //dialog of read ghost book
                            gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialDiary").gameObject.SetActive(true);
                            firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                            gameObject.transform.parent.transform.GetChild(0).transform.GetChild(9).gameObject.SetActive(false);
                            diaryIsOpen = false;
                        }
                        else
                        {
                            firstPersonController.GetComponent<FirstPersonController>().enabled = false;
                            gameObject.transform.parent.transform.GetChild(0).transform.GetChild(9).gameObject.SetActive(true);
                            diaryIsOpen = true;
                        }
                    }

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
                    if (!digitalLockIsOpen)
                    {
                        UnlockInfirmaryDoor();
                        digitalLockIsOpen = true;
                    }
                    else
                    {
                        //Digital password lock panel
                        gameObject.transform.parent.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        digitalPasswordUI.transform.parent.GetComponent<Canvas>().sortingOrder = 0;
                        Cursor.lockState = CursorLockMode.Locked;
                        firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                        digitalLockIsOpen = false;
                    }
                    
                }

                if (digitalLockIsOpen)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        //Digital password lock panel
                        gameObject.transform.parent.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        digitalPasswordUI.transform.parent.GetComponent<Canvas>().sortingOrder = 0;
                        Cursor.lockState = CursorLockMode.Locked;
                        firstPersonController.GetComponent<FirstPersonController>().enabled = true;
                        digitalLockIsOpen = false;
                    }
                        
                }
                
            }
            
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void DoorInteraction()
    {
        RaycastHit hit;

        
        // Check for "E" key being released
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check for a door in front of the player
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpDistance))
            {
                // // Check for a door with a matching tag
                foreach (string doorTag in doorTags)
                {
                    if (hit.collider.gameObject.tag == doorTag)
                    {
                        doorInteraction(hit.collider.gameObject);
                    }
                }
                
                // Check for a multi-door with a matching tag
                // Open or close the door based on its current state
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
                    endIndex = 6;
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
                    if (hit.collider.gameObject.name == "1fFemaleLockedToiletDoor")
                    {
                        if (inventory.transform.Find("1F Female Toilet Key") != null)
                        {
                            hit.collider.gameObject.SetActive(false);
                        }
                        else
                        {
                            DoorLockedDialogue();
                        }
                    }else if (hit.collider.gameObject.name == "PasswordLock")
                    {
                        gameObject.transform.GetChild(2).GetChild(0).Find("dialNeedPwd").gameObject.SetActive(true);
                    }else if (hit.collider.gameObject.name == "LockedLibraryDoor")
                    {
                        if (inventory.transform.Find("LibraryKey") != null)
                        {
                            hit.collider.gameObject.SetActive(false);
                        }
                        else
                        {
                            DoorLockedDialogue();
                        }
                    }else if (hit.collider.gameObject.name == "2fMaleLockedToiletDoor")
                    {
                        if (inventory.transform.Find("MaleToiletKey") != null)
                        {
                            hit.collider.gameObject.SetActive(false);
                        }
                        else
                        {
                            DoorLockedDialogue();
                        }
                    
                    }
                    else
                    {
                        DoorLockedDialogue();
                    }
                    
                }
                else if (hit.collider.gameObject.tag == "Locker")
                {
                    if (!isHide)
                    {
                        temp_position = gameObject.transform;
                        gameObject.transform.position = hit.collider.gameObject.transform.position;
                        HideLocker();
                    }
                    else
                    {
                        UnhideLocker();
                    }
                }else if (hit.collider.gameObject.name == "ToiletSurpriseDoor")
                {
                    gameObject.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).Find("blockvent").gameObject.SetActive(false);
                    // Surprise Dead body
                    //dialog of dialGoingVent
                    gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialGoingVent").gameObject.SetActive(true);
                    AIController.patrolPhase = 1;
                }
                else if (hit.collider.gameObject.tag == "exit")
                {
                    if (CutsceneManager.currentCutscene < 1 && inventory.transform.Find("HorrorDoll") == null && inventory.transform.Find("ExitKey") == null)
                    {
                        CutsceneManager.currentCutscene++;
                        if (CutsceneManager.currentCutscene == 1)
                        {
                            destroyDoor = GameObject.FindWithTag("destroyDoor");
                            destroyDoor.transform.localPosition = new Vector3(0, 0.015f, -1.352f);
                            destroyDoor.transform.localRotation = Quaternion.Euler(-89.386f, 0, 0);
                        }
                        SceneManager.LoadScene(1);
                        //dialog of dialWhenSeeGhost
                        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialWhenSeeGhost").gameObject.SetActive(true);
                    }
                    else if (inventory.transform.Find("HorrorDoll") == null && inventory.transform.Find("ExitKey") == null)
                    {
                        //dialog of dialToOpenExit
                        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialToOpenExit").gameObject.SetActive(true);
                    }
                    else
                    {
                        for (int n = 0; n < gameObject.transform.parent.childCount; n++)
                        {
                            if (gameObject.transform.parent.GetChild(n).gameObject.name != gameObject.name)
                            {
                                Destroy(gameObject.transform.parent.GetChild(n).gameObject);
                            }
                        }
                        SceneManager.LoadScene(3);
                    }
                }else if (hit.collider.gameObject.name == "TriggerBurnEffect")
                {
                    GameObject.Find("Decoration").gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    Destroy(inventory.transform.Find("HorrorDoll").gameObject);
                    GameIsOver = true;
                }
            }
        }

    }
    
    // Item Pickup
    void PickUp(GameObject item)
    {
        if (item.name == "battery")
        {
            Inventory.numBattery++;
            Destroy(item);
        }
        else
        {
            // Add the item to the player's inventory or perform some other action
            item.SetActive(false);
            item.transform.SetParent(inventory.transform);
            Debug.Log("Pick up!");
        
            if (item.name == "ExitKey")
            {
                //dialog of dialGetExitKey
                gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialGetExitKey").gameObject.SetActive(true);
            }

            if (item.name == "HorrorDoll")
            {
                //dialog of dialGetDoll
                gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialGetDoll").gameObject.SetActive(true);
                AIController.patrolPhase = 4;
            }

            if (item.name == "MaleToiletKey")
            {
                //dialog of dialGetDoll
                gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.transform.Find("dialMaleToilet").gameObject.SetActive(true);
            }
        }
        


    }

    // Door Open
    void doorInteraction(GameObject door)
    {
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.clip = openDoorSound[];
        // audio.Play();
        if (door.GetComponent<Animator>().GetBool("Trigger"))
        {
            door.GetComponent<Animator>().SetBool("Trigger", false);
        }
        else
        {
            door.GetComponent<Animator>().SetBool("Trigger", true);
        }
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


    void DoorLockedDialogue()
    {
        gameObject.transform.GetChild(2).GetChild(0).Find("dialDoorLock").gameObject.SetActive(true);
    }
    
    IEnumerator playerTeleportToLab()
    {
        Debug.Log("Entering the vein");
        // Wait for 3 seconds
        yield return new WaitForSeconds(2.5f);
        
        // Do something after 3 seconds
        // Change player checkpoint
        gameObject.transform.localPosition = new Vector3(12.10f, 0.547f, -23.089f);
        gameObject.transform.localRotation = Quaternion.Euler(0f, -23.602f, 0f);
        //Bucket Area Active
        surpriseEventTrigger();
        
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
        //When the player open the female toilet
        AIController.patrolPhase = 2;
        StartCoroutine(playerTeleportToLab());
    }

    // Trigger Infirmary Digital Lock to unlock the Door
    void UnlockInfirmaryDoor()
    {
        digitalPasswordUI.SetActive(true);
        digitalPasswordUI.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
        Cursor.lockState = CursorLockMode.None;
        firstPersonController.GetComponent<FirstPersonController>().enabled = false;
    }

    void HideLocker()
    {
        gameObject.transform.Translate(0, 1.0f, 0);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f ,0.0f);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<FirstPersonController>().playerCanMove = false;
        isHide = true;
    }
    
    void UnhideLocker()
    {
        gameObject.transform.position = temp_position.position;
        gameObject.transform.Translate(0, -1.0f, 0.5f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f ,0.0f);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<FirstPersonController>().playerCanMove = true;
        isHide = false;
    }
    
    //Surprise event
    void surpriseEventTrigger()
    {
        //Bucket Area Active
        gameObject.transform.parent.GetChild(0).gameObject.transform.GetChild(10).transform
            .Find("SurpriseBucket").transform.GetChild(0).gameObject.SetActive(true);
    }

    void gameCompleted()
    {
        //Load cutscene
        Debug.Log("Credit Scene!!!!!!");
    }


}
