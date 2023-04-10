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
    public GameObject door;

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
        quesText.text = "";
        for (int i = 0; i < siklar.Length; i++)
        {
            siklar[i].text = "";
        }

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
        canSelect = true;
    }
    private void Update()
    {
        if (sesMng.GetComponent<SesMng2>().finish == true &&play==false)
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
        canSelect = false;
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            //Doðru


            canSelect = false;
            soruGec();
            

        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[0]);
            yield return new WaitForSeconds(soru2[0].length);
            oldur();

        }
        else if (activeQues == 3)
        {
            sesOynat(soru3[0]);
            yield return new WaitForSeconds(soru3[0].length);
            oldur();
        }
    }

    IEnumerator B()
    {
        canSelect = false;
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {

            sesOynat(soru1[1]);
            yield return new WaitForSeconds(soru1[1].length);
            oldur();
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
            yield return new WaitForSeconds(soru3[1].length);
            oldur();
        }
    }

    IEnumerator C()
    {
        canSelect = false;
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            sesOynat(soru1[2]);
            yield return new WaitForSeconds(soru1[2].length);
            oldur();
        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[2]);
            yield return new WaitForSeconds(soru2[2].length);
            oldur();
        }
        else if (activeQues == 3)
        {
            sesOynat(soru3[2]);
            yield return new WaitForSeconds(soru3[2].length);
            oldur();
        }
    }
    IEnumerator D()
    {
        canSelect = false;
        yield return new WaitForSeconds(0);
        if (activeQues == 1)
        {
            sesOynat(soru1[3]);
            yield return new WaitForSeconds(soru1[3].length);
            oldur();
        }
        else if (activeQues == 2)
        {
            sesOynat(soru2[3]);
            yield return new WaitForSeconds(soru2[3].length);
            oldur();
        }
        else if (activeQues == 3)
        {
            //Doðru
            
            sesOynat(soru3[3]);
            yield return new WaitForSeconds(soru3[3].length);
            door.GetComponent<DoorMng>().isLocked = false;
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
