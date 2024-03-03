using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera targetCamera; // 在Inspector中拖拽你的摄像机到这里

    void Update()
    {
        // 确保有一个摄像机被指定
        if (targetCamera != null)
        {
            // 使对象朝向摄像机
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                targetCamera.transform.rotation * Vector3.up);
        }
    }
}
