using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform cameraTransform; // 相机的Transform
    public Transform playerTransform; // 相机的Transform
    public float sensitivity = 5f; // 鼠标灵敏度
    private float rotationX = 0f;

    void Update()
    {
        UpdateLookDirection();
    }

    void UpdateLookDirection()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 计算垂直方向的旋转
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // 限制视角不超过上下各90度

        // 应用水平旋转到玩家身上
        playerTransform.Rotate(Vector3.up * mouseX);

        // 应用垂直旋转到相机上
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }


    public Vector3 GetLookDirection()
    {
        return cameraTransform.forward;
    }
}
