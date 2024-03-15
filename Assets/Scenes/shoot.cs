using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Transform handTransform; // 手臂的Transform
    public GameObject basketballPrefab; // 篮球的Prefab
    private GameObject basketballInstance; // 场景中的篮球实例

    void Start()
    {
        // 实例化篮球并将其作为手臂的子对象
        basketballInstance = Instantiate(basketballPrefab, handTransform.position, Quaternion.identity);
        basketballInstance.transform.SetParent(handTransform);
    }

    // 从动画事件调用这个函数来释放并投掷篮球
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // 释放篮球

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 计算初始速度，这里需要自行调整以符合预期的抛物线
            Vector3 initialVelocity = new Vector3(0, 10, 5);
            rb.velocity = initialVelocity;
            rb.isKinematic = false; // 确保篮球的Rigidbody不是Kinematic
        }
    }
}