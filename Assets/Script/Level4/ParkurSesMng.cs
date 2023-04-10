using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ParkurSesMng : MonoBehaviour
{
    public static ParkurSesMng instance;
    public float sceneIndex;

    public AudioClip[] sesler;
    private AudioSource sound;
    public bool isFinish = false;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        check();
    }
    void Start()
    {
        StartCoroutine(go());
    }

    IEnumerator go()
    {
        for (int i = 0; i < sesler.Length; i++)
        {
            sound.clip = sesler[i];
            sound.Play();
            yield return new WaitForSeconds(sesler[i].length);
        }
        isFinish = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
