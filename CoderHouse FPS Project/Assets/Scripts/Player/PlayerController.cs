using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform playerCameraPov;

        [SerializeField] float mouseSensitivity = 3.5f;
        [SerializeField] [Range(0, 0.5f)] float mouseSmoothing = 0.02f;
        [SerializeField] bool CursorCentered = true;

        [SerializeField] float walkSpeed = 5.0f;
        [SerializeField] float runningSpeed = 20.0f;


        [SerializeField] [Range(0, 0.5f)] float moveSmoothing = 0.35f;
        [SerializeField] float gravity = -10.0f;
        [SerializeField] float velocityY;
        [SerializeField] float jumpingSpeed;

        float cameraAngle;
        CharacterController controller = null;

        Vector2 currentDirection = Vector2.zero;
        Vector2 currentDirectionVelocity = Vector2.zero;

        Vector2 currentMouseDelta = Vector2.zero;
        Vector2 currentMouseDeltaVelocity = Vector2.zero;



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
            MouseLook();
            PlayerMovement();
        }


        private void MouseLook()
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothing);

            cameraAngle -= currentMouseDelta.y * mouseSensitivity;

            cameraAngle = Mathf.Clamp(cameraAngle, -90.0f, 90.0f);

            playerCameraPov.localEulerAngles = Vector3.right * cameraAngle;

            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
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
            velocityY += gravity * Time.deltaTime;

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
}