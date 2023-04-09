using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPCTask : MonoBehaviour
{
    private GenelManager mng;
    public bool isWork = false;
    public AudioClip[] konusmalar;
    private AudioSource sound;


    public AudioClip GorevliSes;
    public Transform gidecegiKonum;
    public Transform player;
    public GameObject gorunmezEngel;
    

    private Animator anim;

    private Transform target;

    private void Update()
    {
       
        Vector3 direction = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
        transform.rotation = Quaternion.LookRotation(direction);
    }
    private void Awake()
    {
        target = this.gameObject.transform;
        mng = GameObject.Find("Manager").GetComponent<GenelManager>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mng.taskNum == 4 && isWork == false)
            {
                StartCoroutine(task());
                isWork = true;
            }
        }
    }

    IEnumerator task()
    {
        //Karakterin hareket etmesini engelle
        mng.player.GetComponent<Movement>().canMove = false;
        gorunmezEngel.SetActive(true);
        target = player;
        for (int i = 0; i < konusmalar.Length; i++)
        {
            sound.clip = konusmalar[i];
            sound.Play();
            if(i%2!=0)
            {
                anim.SetBool("talk", true);
            }
            else
            {
                anim.SetBool("talk", false);
            }
            yield return new WaitForSeconds(konusmalar[i].length);
        }
        AudioSource.PlayClipAtPoint(GorevliSes, transform.position);
        anim.SetBool("talk", false);
        anim.SetBool("walk", true);
        target = gidecegiKonum;
        mng.yemekhaneKapi.GetComponent<Animator>().SetBool("character_nearby", true);
        transform.DOMove(gidecegiKonum.position, 5f);

        mng.player.GetComponent<Movement>().canMove = true;
        mng.DersKapi.GetComponent<DoorMng>().isLocked = true;
        yield return new WaitForSeconds(3);
        mng.yemekhaneKapi.GetComponent<Animator>().SetBool("character_nearby", false);
        //karakterin hareket etmesini aç
       

        mng.taskText.text = "";
        mng.taskText.text = "<color=red>CEPHANELIGE HEMEN GIT !!!</color>";
        mng.taskNum = 5;
        
        
    }
}
