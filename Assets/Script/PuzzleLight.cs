using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleLight : MonoBehaviour
{

    [SerializeField] string rightTag;
    [SerializeField] Light light;
    [SerializeField] GameObject letter;
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material newMaterial;

    [SerializeField] String correctName;

    [SerializeField] GameObject kapi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name == correctName)
        {
            Debug.Log(other.transform.gameObject.name + "Girdi");
            light.intensity = 2;
            letter.transform.position = new Vector3(currentPosition.x, currentPosition.y, -19.95f);
            letter.transform.gameObject.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
            kapi.gameObject.GetComponent<LevelKapi>().correctCount ++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.name == correctName)
        {
            Debug.Log(other.transform.gameObject.name + "Çýktý.");
            light.intensity = 10;
            letter.transform.position = currentPosition;
            letter.transform.gameObject.GetComponent<Renderer>().materials[1] = defaultMaterial;
            letter.transform.gameObject.GetComponent<Renderer>().materials[1].DisableKeyword("_EMISSION");
            kapi.gameObject.GetComponent<LevelKapi>().correctCount--;
        }
    }
}
