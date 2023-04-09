using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgitmenTask : MonoBehaviour
{
    public GenelManager mng;
    public bool isWork = false;
    public AudioClip[] konusmalar;
    public GameObject gun;

    private AudioSource sound;
    private Animator anim;

    private void Awake()
    {
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mng.taskNum == 1 && isWork == false)
            {
                StartCoroutine(task());
                isWork = true;
            }
        }
    }

    IEnumerator task()
    {
        
        mng.taskNum = 2;
        sound.clip = konusmalar[0];
        sound.Play();
        anim.SetTrigger("angry");
        yield return new WaitUntil(() => gun.GetComponent<Gun>().hasGun==true);
        anim.SetTrigger("idle");
        mng.taskNum = 3;
        
    }
}
