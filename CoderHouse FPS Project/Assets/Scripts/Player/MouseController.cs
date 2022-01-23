using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] Transform playerCameraPov;

    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] [Range(0, 0.5f)] float mouseSmoothing = 0.02f;

    private float cameraAngle;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothing);

        cameraAngle -= currentMouseDelta.y * mouseSensitivity;

        cameraAngle = Mathf.Clamp(cameraAngle, -90.0f, 90.0f);

        playerCameraPov.localEulerAngles = Vector3.right * cameraAngle;

        player.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
}
