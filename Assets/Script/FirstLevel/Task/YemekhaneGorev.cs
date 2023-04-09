using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemekhaneGorev : MonoBehaviour
{
    public GenelManager mng;
    public bool isWork = false;
    public AudioClip[] konusmalar;
    private Animator anim;

    private AudioSource sound;
    public Transform target;

    private void Awake()
    {
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
        anim = GetComponentInChildren<Animator>();
        sound = GetComponent<AudioSource>();
        target = null;
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
    private void Update()
    {
        if(target!=null)
        {
            Vector3 direction = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
    }

    IEnumerator task()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("angry");
        target = mng.player.transform;
        for (int i = 0; i < konusmalar.Length; i++)
        {
            sound.clip = konusmalar[i];
            sound.Play();
            yield return new WaitForSeconds(konusmalar[i].length);
        }
        
        yield return new WaitForSeconds(2);
        target = null;
        mng.taskNum = 1;
    }
}
