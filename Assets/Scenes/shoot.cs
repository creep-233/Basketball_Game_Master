using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Transform handTransform; // 手臂的Transform
    public GameObject basketballPrefab; // 篮球的Prefab
    private GameObject basketballInstance; // 场景中的篮球实例

    void Initialise()
    {
        // 实例化篮球并将其作为手臂的子对象
        // 增加的高度值
        float additionalHeight = 0.213f; // 这里设置你想要增加的高度值

        // 实例化篮球，并在手部Transform的基础上增加一些高度
        Vector3 spawnPosition = handTransform.position + new Vector3(0, additionalHeight, 0);
        basketballInstance = Instantiate(basketballPrefab, spawnPosition, Quaternion.identity);
        basketballInstance.transform.SetParent(handTransform);


        // 禁用篮球的重力
        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // 禁用重力
            rb.isKinematic = true; // 将篮球设置为Kinematic以避免物理影响
        }
    }

    // 从动画事件调用这个函数来释放并投掷篮球
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // 释放篮球

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // 启用重力
            rb.isKinematic = false; // 确保篮球的Rigidbody不是Kinematic

            // 计算初始速度，这里需要自行调整以符合预期的抛物线
            Vector3 initialVelocity = new Vector3(0, 10, 5);
            rb.velocity = initialVelocity;
        }
    }
}