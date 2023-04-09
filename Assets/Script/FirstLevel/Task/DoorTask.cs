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
            isWork = true;
            if (mng.taskNum == 7)
            {
                anim.SetBool("character_nearby", true);
            }
        }
        
    }
}
