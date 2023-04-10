using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class NextLevel : MonoBehaviour
{
    private Scene _scene;
    public bool canDelObj = false;
    public string objName;
    private GameObject obj;

    public Image black;
   

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
        obj = GameObject.Find(objName);
        black = GameObject.Find("Black").GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            black.DOFade(1,1f).OnComplete(()=> SceneManager.LoadScene(_scene.buildIndex + 1));
            if(canDelObj==true)
            {
                Destroy(obj);
            }
        }
    }
}

