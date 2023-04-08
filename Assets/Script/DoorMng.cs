using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMng : MonoBehaviour
{
    private Animator anim;
    public bool isLocked = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isLocked == false)
            {
                Open();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            Close();
        }
    }

    public void Open()
    {
        anim.SetBool("character_nearby", true);
    }
    public void Close()
    {
        anim.SetBool("character_nearby", false);
    }
}
