using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SesMng2 : MonoBehaviour
{
   
    

    public AudioClip[] sesler;
    public AudioSource sound;

    public bool isPlay = false;
    static SesMng2 instance;
    public int sceneIndex;
    
    private void Awake()
    {
        check();

        sound = GetComponent<AudioSource>();
       



    }



    public void check()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (sceneIndex == 0)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        if (sceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            Debug.Log("farklý");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("ayný" + " " + SceneManager.GetActiveScene().buildIndex);

        }
    }





    IEnumerator go()
    {
        for (int i = 0; i < sesler.Length; i++)
        {
            sound.clip = sesler[i];
            sound.Play();
            yield return new WaitForSeconds(sesler[i].length);
        }
        isPlay = true;
    }

    private void Start()
    {
        if(isPlay==false)
        {
            StartCoroutine(go());
        }
        
    }
}
