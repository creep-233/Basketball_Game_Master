using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Camera playerCamera; // 添加一个对玩家摄像机的引用
    private Vector3 initialVelocity; // 初始化时不赋值
    private float initialSpeed = 10f; // 初始速度的大小
    private float speedAdjustment = -0.5f; // 每次滚轮滚动时速度的调整值
    public Transform handTransform; // 手臂的Transform
    public GameObject basketballPrefab; // 篮球的Prefab
    private GameObject basketballInstance; // 场景中的篮球实例
    public LineRenderer lineRenderer;
    private bool isHoldingBall = true;
    
    void Start()
    {
        lineRenderer.positionCount = 0;
        // 初始化时，不显示任何线段 }
        initialVelocity = playerCamera.transform.forward * initialSpeed;
    }
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
        ShowTrajectory();
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            initialVelocity -= new Vector3(0, 1f, 0);
            initialSpeed = Mathf.Max(1f, initialSpeed); // 确保速度不会小于1
        }

        // 更新速度方向
        initialVelocity = playerCamera.transform.forward * initialSpeed + playerCamera.transform.up * 5;
        if (isHoldingBall)
        {
            // 检查是否按下了投掷按键
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReleaseAndShootBall();
                isHoldingBall = false;
                lineRenderer.positionCount = 0; // 投掷后不显示抛物线
            }
           
        }
    }

    void ShowTrajectory()
    {
        int segmentCount = 20; // 抛物线上的线段数
        float segmentScale = 0.1f; // 每个线段之间的时间间隔
        Vector3[] segments = new Vector3[segmentCount];

        segments[0] = basketballInstance.transform.position;

        // 初始速度和方向
        Vector3 segVelocity = initialVelocity;

        for (int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * segmentScale;
            segments[i] = segments[0] + segVelocity * elapsedTime + 0.5f * Physics.gravity * Mathf.Pow(elapsedTime, 2);
        }

        lineRenderer.positionCount = segmentCount;
        lineRenderer.SetPositions(segments);
    }

    // 从动画事件调用这个函数来释放并投掷篮球
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // 释放篮球
        ShowTrajectory();

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // 启用重力
            rb.isKinematic = false; // 确保篮球的Rigidbody不是Kinematic

            // 计算初始速度，这里需要自行调整以符合预期的抛物线
            
            rb.velocity = initialVelocity;
        }
    }
}