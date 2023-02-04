using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject torchLight;
    public bool gotLight = false;

    public static int numBattery = 0;
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
            inventoryCount = gameObject.transform.childCount;
        }
    }

}
