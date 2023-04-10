using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTask : MonoBehaviour
{
    private GenelManager mng;
    public AudioClip[] sesler;
    public bool isWork = false;
    

    private AudioSource sound;
    private Animator anim;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
        anim = GetComponentInParent<Animator>();
    }

    public void open()
    {   
        if(isWork==false)
        {
           
            if (mng.taskNum == 7)
            {
                isWork = true;
                anim.SetBool("character_nearby", true);
                mng.taskNum = 8;
            }
        }
        
    }
}
