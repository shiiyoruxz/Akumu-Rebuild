using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    private float pollingTime = 1.0f;
    private float time;
    private int frameCount;
    private int frameRate;

    public TextMeshProUGUI FPSText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            frameRate = Mathf.RoundToInt(frameCount / time);
            FPSText.text = frameRate.ToString() + " FPS";
            
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
