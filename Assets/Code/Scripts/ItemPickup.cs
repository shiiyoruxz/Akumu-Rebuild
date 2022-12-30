using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickUpDistance = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
