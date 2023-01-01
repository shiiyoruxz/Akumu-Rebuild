using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject torchLight;
    public bool gotLight = false;
    public int numBattery = 1;

    public int inventoryCount;

    private string torchlight;
    // Start is called before the first frame update
    void Start()
    {

        inventoryCount = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount > inventoryCount)
        {
            Debug.Log("added");
            inventoryCount = gameObject.transform.childCount;
        }
        updateInventory();
    }

    void updateInventory()
    {
        if (gameObject.transform.Find("Battery"))
        {
            torchLight.SetActive(true);
            
        }
        else
        {
            torchLight.SetActive(false);
        }
    }
    
}
