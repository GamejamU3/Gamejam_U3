using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotasyonSabit : MonoBehaviour
{

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
