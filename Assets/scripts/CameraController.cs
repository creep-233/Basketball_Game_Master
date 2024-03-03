using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public float clampAngle = 70f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        player.Rotate(Vector3.up * rotationY);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    void OnGUI()
    {
        DrawCrosshair();
    }

    void DrawCrosshair()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float crosshairSize = 20f;

        float crosshairX = screenWidth / 2 - crosshairSize / 2;
        float crosshairY = screenHeight / 2 - crosshairSize / 2;

        GUI.DrawTexture(new Rect(crosshairX, crosshairY, crosshairSize, 2), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(crosshairX, crosshairY, 2, crosshairSize), Texture2D.whiteTexture);
    }
}
