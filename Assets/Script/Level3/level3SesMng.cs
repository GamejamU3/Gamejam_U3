using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3SesMng : MonoBehaviour
{
    public AudioClip[] sesBaslangic;
    public AudioClip[] sesBitis;
    public AudioSource sound;

    public bool finish = false;
    
   
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(go());
    }
    IEnumerator go()
    {
        for (int i = 0; i < sesBaslangic.Length; i++)
        {
            sound.clip = sesBaslangic[i];
            sound.Play();
            yield return new WaitForSeconds(sesBaslangic[i].length);
        }

        yield return new WaitUntil(() => finish==true);

        for (int i = 0; i < sesBitis.Length; i++)
        {
            sound.clip = sesBitis[i];
            sound.Play();
            yield return new WaitForSeconds(sesBitis[i].length);
        }
    }
}
