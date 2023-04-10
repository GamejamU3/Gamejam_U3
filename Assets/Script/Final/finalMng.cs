using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class finalMng : MonoBehaviour
{
    public Image black;
    public string metin;
    public float yazibeklemesure;
   
    public TextMeshProUGUI text;
    public TextMeshProUGUI textAnyKey;
    public bool canGo = false;
    public bool finisg = false;

    void Start()
    {
        
        black.DOFade(0, 1f).From(1);
        textAnyKey.DOFade(0, 0);
        StartCoroutine(go());

    }

    // Update is called once per frame
    void Update()
    {
        if (finisg == true)
        {
            if (Input.anyKeyDown)
            {
                canGo = true;
            }
        }
    }
    IEnumerator go()
    {
        foreach (char a in metin)
        {
            text.text += a.ToString();
            yield return new WaitForSeconds(yazibeklemesure);

        }
        textAnyKey.DOFade(1, 5);
        finisg = true;
        yield return new WaitUntil(() => canGo == true);
        black.DOFade(1, 1).From(0).OnComplete(() => SceneManager.LoadScene(0));
    }
}
