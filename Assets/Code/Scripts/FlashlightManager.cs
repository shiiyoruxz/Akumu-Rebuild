using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TerrainTools;

public class FlashlightManager : MonoBehaviour
{
    public GameObject torchlight;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    
    public static bool firstBattery = true;
    private bool _flashlightToggle = false;
    private bool _flashlightOnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (torchlight.activeSelf == false && firstBattery == true && Inventory.numBattery > 0)
        {
            _flashlightOnOff = true;
            firstBattery = false;
            torchlight.SetActive(true);
        }

        torchlight.GetComponent<Light>().intensity = Mathf.Clamp(torchlight.GetComponent<Light>().intensity,
            minBrightness, maxBrightness);
        
        if (torchlight.activeSelf == true && drainOverTime == true && torchlight.GetComponent<Light>().enabled)
        {
            if (torchlight.GetComponent<Light>().intensity > minBrightness)
            {
                torchlight.GetComponent<Light>().intensity -= Time.deltaTime * (drainRate / 1000);
            }
        }

        if (torchlight.activeSelf == true && Input.GetKeyDown(KeyCode.F) && EscPauseGame.gameIsPaused == false)
        {
            if (_flashlightOnOff == true)
            {
                 AudioManager.Instance.PlaySFX("FlashlightOff");
                 torchlight.GetComponent<Light>().enabled = false;
                 _flashlightOnOff = false;
            }
            else
            {
                 AudioManager.Instance.PlaySFX("FlashlightOn");
                 torchlight.GetComponent<Light>().enabled = true;
                 _flashlightOnOff = true;
            }
        }

        if (torchlight.activeSelf == true && Input.GetKeyDown(KeyCode.R))
        {
            ReplaceBattery();
        }

        if (EscPauseGame.gameIsPaused == true)
        {
            if (torchlight.GetComponent<Light>().enabled)
            {
                torchlight.GetComponent<Light>().enabled = false;
                _flashlightToggle = true;
            }
        }
        else
        {
            if (_flashlightToggle)
            {
                torchlight.GetComponent<Light>().enabled = true;
                _flashlightToggle = false;
            }
        }
    }

    public void ReplaceBattery()
    {
        if (Inventory.numBattery > 0)
        {
           torchlight.GetComponent<Light>().intensity = maxBrightness;
            Inventory.numBattery -= 1;
        }
    }
}
