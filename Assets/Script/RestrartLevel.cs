using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestrartLevel : MonoBehaviour
{
    private Scene _scene;

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("door_1").GetComponent<SesMng>().olumSayisi++;
            SceneManager.LoadScene(_scene.buildIndex);
        }
    }
}
