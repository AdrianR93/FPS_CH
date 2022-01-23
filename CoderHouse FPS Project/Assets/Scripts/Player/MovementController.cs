using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] bool CursorCentered = true;

    [SerializeField] float walkSpeed = 5.0f;

    [SerializeField] float runningSpeed = 20.0f;

    [SerializeField] [Range(0, 0.5f)] float moveSmoothing = 0.35f;

    [SerializeField] float gravity = -10.0f;

    [SerializeField] float velocityY;

    [SerializeField] float jumpingSpeed;

    CharacterController controller = null;

    Vector2 currentDirection = Vector2.zero;

    Vector2 currentDirectionVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (CursorCentered)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void Update()
    {
        PlayerMovement();
    }



    private void PlayerMovement()
    {


        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) && controller.isGrounded)
        {
            speed = runningSpeed;
        }

        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, moveSmoothing);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        Vector3 velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * speed + Vector3.up * velocityY;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocityY = jumpingSpeed;
        }

        velocityY += gravity * Time.deltaTime;

        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);


    }
}
