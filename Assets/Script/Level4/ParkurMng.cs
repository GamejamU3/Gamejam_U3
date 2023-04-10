using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParkurMng : MonoBehaviour
{
    public ParkurSesMng sesMng;
    public float maxDuration;
    public float currentDuration;

    public Image bar;
    private void Awake()
    {
        sesMng=GameObject.Find("sesMng").GetComponent<ParkurSesMng>();
        currentDuration = maxDuration;
    }
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(sesMng.isFinish==true)
        {
            if(bar.gameObject.activeInHierarchy==false)
            {
                bar.gameObject.SetActive(true);
            }
            currentDuration -= Time.deltaTime;
            bar.fillAmount = currentDuration / maxDuration;

            if(currentDuration<=0)
            {
                oldur();
            }

        }
    }

    public void oldur()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
