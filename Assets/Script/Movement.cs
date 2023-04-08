using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Player Control Settings")]
    private float verticalInput;
    private float horizontalInput;

    CharacterController controller;

    [SerializeField] float walkSpeed=8f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpPower = 0.25f;
    private float currentSpeed = 8f;

    private Vector3 heighMovement;

    private bool jump = false;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded){
            jump= true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

    }

    void FixedUpdate()
    {
        if (jump)
        {
            heighMovement.y = jumpPower;
            jump = false;
        }

        heighMovement.y -= gravity * Time.deltaTime;

        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;
        direction.Normalize();

        direction *= walkSpeed * Time.deltaTime;

        controller.Move( direction +  heighMovement);


        if (controller.isGrounded)
        {
            heighMovement.y = 0f;
        }
    }
}
