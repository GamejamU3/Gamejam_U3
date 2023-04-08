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

    public GameObject DersKapi;


    public Transform[] botSpawnPos;
    public GameObject bot;
    public int dieBotCount;

    [Space(10)]
    public int taskNum = 0;
    // 0 -> Yemekhaneye git
    // 1 -> Atýþ talimine git 
    // 2 -> Silahýný çek
    // 3-> 10 tane bot vur
    // 4 -> Cephaneye git ve silahýný býrak
   

    void Start()
    {
        black.GetComponent<Image>().DOFade(0, 1f).From(1f);
        taskText.text = "";
        StartCoroutine(gorev());
       
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
        StartCoroutine(changeTask());
        DersKapi.GetComponent<DoorMng>().isLocked = true;
        yield return new WaitUntil(() => taskNum == 1);
        DersKapi.GetComponent<DoorMng>().isLocked = false;
        StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 2);
        DersKapi.GetComponent<DoorMng>().isLocked = true;
        StartCoroutine(changeTask());
        yield return new WaitUntil(() => taskNum == 3);
        StartCoroutine(changeTask());
        spawnBot();
        yield return new WaitUntil(() => taskNum == 4);
        DersKapi.GetComponent<DoorMng>().isLocked = false;
        StartCoroutine(changeTask());
    }

    public void spawnBot()
    {
        Instantiate(bot, botSpawnPos[Random.Range(0, botSpawnPos.Length)].position, Quaternion.Euler(0, 90, 0));
    }
}
