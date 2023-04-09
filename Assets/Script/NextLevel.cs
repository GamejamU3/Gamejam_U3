using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private Scene _scene;
    public bool canDelObj = false;
    public string objName;
    private GameObject obj;

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
        obj = GameObject.Find(objName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(_scene.buildIndex + 1);
            if(canDelObj==true)
            {
                Destroy(obj);
            }
        }
    }
}

