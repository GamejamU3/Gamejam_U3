using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SesMng : MonoBehaviour
{
    public Animator anim;
    public bool isWork = false;

    public AudioClip[] sesler;
    public AudioSource sound;

    public bool isPlay = false;
    static SesMng instance;
    public int sceneIndex;
    private void Awake()
    {
        check();
      
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
       
        
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

    private void Update()
    {
        if (isWork == false && anim.GetBool("character_nearby") == true && isPlay==false)
        {
            isWork = true;
            StartCoroutine(go());
        }
    }
}
