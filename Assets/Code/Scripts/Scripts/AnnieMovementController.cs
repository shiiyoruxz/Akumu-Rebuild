using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnieMovementController : MonoBehaviour
{
    private Animator anim;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float z;
        
        if(Input.GetAxis("Vertical") == 0.0f)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsWalkBack", false);
        }
        if(Input.GetAxis("Vertical") > 0.0f)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalkBack", false);
            anim.SetBool("IsWalking", true);
        }
        if(Input.GetAxis("Vertical") < 0.0f)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsWalkBack", true);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 4; 
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", true);
        } 
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", true);
        } 
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("IsCrawling", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("IsCrawling", false);
            // anim.SetBool("IsRunning", false);
            // anim.SetBool("IsWalking", true);
        } 

        z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(0, 0, z);
    }
}
