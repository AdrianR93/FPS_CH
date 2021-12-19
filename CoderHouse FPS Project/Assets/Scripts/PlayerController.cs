using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCameraPov;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] bool CursorCentered = true;

    float cameraAngle;

    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] [Range(0, 0.5f)] float moveSmoothing = 0.35f;
    [SerializeField] [Range(0, 0.5f)] float mouseSmoothing = 0.02f;

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
        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, moveSmoothing);

        Vector3 velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * movementSpeed;



        controller.Move(velocity * Time.deltaTime);


    }
}
