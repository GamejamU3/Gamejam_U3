using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesOynatici : MonoBehaviour
{
    private GenelManager mng;
    public bool isWork = false;
    public int olduguAsama;
    public bool asamaGecicekMi = false;
    public bool kilitleyecekMi = false;

    public AudioClip[] sesler;
    private AudioSource sound;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(isWork==false && olduguAsama==mng.taskNum)
            {
                StartCoroutine(sesiOynat());
                isWork = true;
            }
        }
    }

    IEnumerator sesiOynat()
    {
        if(kilitleyecekMi==true)
        {
            mng.player.GetComponent<Movement>().canMove = false;
        }
        for (int i = 0; i < sesler.Length; i++)
        {
            sound.clip = sesler[i];
            sound.Play();

            yield return new WaitForSeconds(sesler[i].length);
        }

        if(asamaGecicekMi==true)
        {
            mng.taskNum = olduguAsama + 1;
        }
        if (kilitleyecekMi == true)
        {
            mng.player.GetComponent<Movement>().canMove = true;
        }

    }
}
