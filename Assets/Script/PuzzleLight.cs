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


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(rightTag))
        {
            light.intensity = 2;
            letter.transform.position = new Vector3(currentPosition.x, currentPosition.y, -19.90f);
            letter.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials[1] = newMaterial;

        }
        else
        {
            light.intensity = 10;
            letter.transform.position = currentPosition;
            letter.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials[1] = defaultMaterial;
        }
    }
}
