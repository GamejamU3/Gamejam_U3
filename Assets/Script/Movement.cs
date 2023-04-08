using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController controller;

    [SerializeField] float speed;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;
     
        controller.Move(Vector3.Normalize(direction) * speed * Time.deltaTime);
    }
}
