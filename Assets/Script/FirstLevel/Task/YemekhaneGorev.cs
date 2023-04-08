using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemekhaneGorev : MonoBehaviour
{
    public GenelManager mng;
    public bool isWork = false;
    public AudioClip[] konusmalar;

    private AudioSource sound;

    private void Awake()
    {
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(mng.taskNum==0 && isWork==false)
            {
                StartCoroutine(task());
                isWork = true;
            }
        }
    }

    IEnumerator task()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Konuþma animasyon devreye girer");
        for (int i = 0; i < konusmalar.Length; i++)
        {
            sound.clip = konusmalar[i];
            sound.Play();
            yield return new WaitForSeconds(konusmalar[i].length);
        }
        yield return new WaitForSeconds(2);
        mng.taskNum = 1;
    }
}
