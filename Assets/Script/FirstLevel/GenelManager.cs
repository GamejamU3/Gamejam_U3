using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class GenelManager : MonoBehaviour
{

    public GameObject black;
    public TextMeshProUGUI taskText;
    public string[] task;
    public float yaziBeklemeSure;
    private Coroutine chngtsk;
    public GameObject player;
    [Space(10)]
    [Header("Kap�lar")]
    public GameObject DersKapi;
    public GameObject yemekhaneKapi;
    public GameObject cephaneKapi;

    [Space(10)]
    [Header("Bot")]
    public Transform[] botSpawnPos;
    public GameObject bot;
    public int dieBotCount;
    [Space(10)]
    [Header("NPC")]
    public GameObject NPC;
    [Header("Cephanelik")]
    public GameObject silahOnIzleme;
    [Header("SonKonusma")]
    public AudioClip[] sesler;
    private AudioSource sound;
    public string[] bozukYazi;

    [Space(10)]
    public int taskNum = 0;
    // 0 -> Yemekhaneye git
    // 1 -> At�� talimine git 
    // 2 -> Silah�n� �ek
    // 3-> 10 tane bot vur
    // 4 -> Cephaneye git ve silah�n� b�rak
    // 5 -> CEPHANEYE G�T !!!
    // 6 -> Kap�ya yakla�
    // 7 -> Kap�ya ate� et
    // 8 -> Kap�dan i�eri gir
   

    void Start()
    {
        black.GetComponent<Image>().DOFade(0, 1f).From(1f);
        taskText.text = "";
        StartCoroutine(gorev());
        NPC.SetActive(false);
        silahOnIzleme.SetActive(false);
        player = GameObject.Find("Player");
        sound = gameObject.GetComponent<AudioSource>();
    }
  

    IEnumerator changeTask()
    {
        taskText.text = "";
        foreach (char i in task[taskNum])
        {
            taskText.text += i.ToString();
            yield return new WaitForSeconds(yaziBeklemeSure);

        }
    }
    IEnumerator gorev()
    {
        chngtsk=StartCoroutine(changeTask());
        DersKapi.GetComponent<DoorMng>().isLocked = true;
        yield return new WaitUntil(() => taskNum == 1);
        DersKapi.GetComponent<DoorMng>().isLocked = false;
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 2);
        DersKapi.GetComponent<DoorMng>().isLocked = true;
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 3);
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        spawnBot();
        yield return new WaitUntil(() => taskNum == 4);
        DersKapi.GetComponent<DoorMng>().isLocked = false;
        NPC.SetActive(true);
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 5);
        //bu sefer change task yok ��nk� sert yazd�r�cam
        cephaneKapi.GetComponent<DoorMng>().isLocked = false;
        silahOnIzleme.SetActive(true);
        yield return new WaitUntil(() => taskNum == 6);
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        silahOnIzleme.SetActive(false);
        yield return new WaitUntil(() => taskNum == 7);
        StopCoroutine(chngtsk);
        chngtsk = StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 8);

        player.GetComponent<Movement>().canMove = false;

        for (int i = 0; i < sesler.Length; i++)
        {
            sound.clip = sesler[i];
            sound.Play();
            yield return new WaitForSeconds(sesler[i].length);
        }

        player.GetComponent<Movement>().canMove = true;
        taskNum = 9;
        yield return new WaitUntil(() => taskNum == 9);
        StartCoroutine(bozul());

    }

    IEnumerator bozul()
    {
        while(true)
        {
            taskText.text = "";
            foreach (char i in bozukYazi[Random.Range(0,bozukYazi.Length)])
            {
                taskText.text += i.ToString();
                yield return new WaitForSeconds(yaziBeklemeSure);

            }
        }
        
    }

    public void spawnBot()
    {
        Instantiate(bot, botSpawnPos[Random.Range(0, botSpawnPos.Length)].position, Quaternion.Euler(0, 90, 0));
    }
}
