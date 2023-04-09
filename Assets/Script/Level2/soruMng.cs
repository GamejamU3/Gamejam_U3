using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class soruMng : MonoBehaviour
{
    public GameObject sesMng;
    public int activeQues = 1;
    public AudioSource sound;
    private Coroutine chngQues;
    private bool play = false;

    [Header("Text")]
    public TextMeshProUGUI quesText;
    public float yaziBeklemeSure;

    [Space(10)]
    [Header("Sorular")]
    public AudioClip[] sorularAu;
    public string[] sorularStr;
    public bool soruYazildiMi = false;

    [Space(10)]
    [Header("CevaplarAu")]
    public AudioClip[] soru1;
    public AudioClip[] soru2;
    public AudioClip[] soru3;
    public bool canSelect = true;

    [Header("CevaplarTxt")]
    public TextMeshProUGUI[] siklar;
    public string[] soru1Txt;
    public string[] soru2Txt;
    public string[] soru3Txt;

    private void Awake()
    {
        sesMng = GameObject.Find("sesMng");
        sound = GetComponent<AudioSource>();
       

    }
    


    IEnumerator soruYaz()
    {
        soruYazildiMi = false;
        sesOynat(sorularAu[activeQues - 1]);
        quesText.text = "";
        for (int i = 0; i < siklar.Length; i++)
        {
            siklar[i].text = "";
        }

        foreach (char i in sorularStr[activeQues-1])
        {
            quesText.text += i.ToString();
            yield return new WaitForSeconds(yaziBeklemeSure);

        }

        for (int i = 0; i < siklar.Length; i++)
        {
            if(activeQues==1)
            {
                foreach (char a in soru1Txt[i])
                {
                    siklar[i].text += a.ToString();
                    yield return new WaitForSeconds(yaziBeklemeSure);

                }
            }
            if (activeQues == 2)
            {
                foreach (char a in soru2Txt[i])
                {
                    siklar[i].text += a.ToString();
                    yield return new WaitForSeconds(yaziBeklemeSure);

                }
            }
            if (activeQues == 3)
            {
                foreach (char a in soru3Txt[i])
                {
                    siklar[i].text += a.ToString();
                    yield return new WaitForSeconds(yaziBeklemeSure);

                }
            }
        }
        soruYazildiMi = true;
    }
    private void Update()
    {
        if (sesMng.GetComponent<SesMng2>().isPlay == true &&play==false)
        {
            play = true;

            chngQues = StartCoroutine(soruYaz());

        }
    }
    public void soruGec()
    {
        StopCoroutine(chngQues);
        activeQues++;
        chngQues = StartCoroutine(soruYaz());

    }

    #region Cevaplar
    public void selectA()
    {
        StartCoroutine(A());
    }

    public void selectB()
    {
        StartCoroutine(B());
    }

    public void selectC()
    {
        StartCoroutine(C());
    }

    public void selectD()
    {
        StartCoroutine(D());
    }

    #endregion

    #region cevaplar IE
    IEnumerator A()
    {
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            //Doðru
            

           
            soruGec();

        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[0]);

        }
        else if (activeQues == 3)
        {
            sesOynat(soru3[0]);
        }
    }

    IEnumerator B()
    {
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            sesOynat(soru1[1]);
        }
        else if (activeQues == 2)
        {
            //Doðru
            sesOynat(soru2[1]);
            yield return new WaitForSeconds(soru2[1].length);
            soruGec();
        }
        else if (activeQues == 3)
        {
            sesOynat(soru3[1]);
        }
    }

    IEnumerator C()
    {
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            sesOynat(soru1[2]);
        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[2]);
        }
        else if (activeQues == 3)
        {
            sesOynat(soru3[2]);
        }
    }
    IEnumerator D()
    {
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            sesOynat(soru1[3]);
        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[3]);
        }
        else if (activeQues == 3)
        {
            //Doðru

            sesOynat(soru3[3]);
            yield return new WaitForSeconds(soru3[3].length);
            soruGec();
        }
    }

    #endregion

    public void sesOynat(AudioClip ses)
    {
        sound.Stop();
        sound.clip = ses;
        sound.Play();
    }

    public void oldur()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
