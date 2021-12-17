/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] private float speed = 12.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpDistance = 3.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.04f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    [SerializeField] bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpDistance * -2f * gravity);
            Debug.Log($"JumpDistance {jumpDistance}");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}*/
