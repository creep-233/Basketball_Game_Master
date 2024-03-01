using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerCameraController cameraController; // PlayerCameraController的引用
    public float minPower = 0f; // 最小蓄力
    public float maxPower = 20f; // 最大蓄力
    public float powerIncreaseRate = 5f; // 蓄力速率
    public Slider powerSlider; // 对应的Slider UI 元素
    public float stepSoundInterval = 0.5f; // 步行音效播放的间隔时间
    private float lastStepTime; // 上次播放步行音效的时间
    private bool isJumping; // 是否正在跳跃
    private Transform shotPoint; // 发射点
    private Vector3 throwDirection; // 扔出篮球的初始方向
    private float currentPower; // 当前蓄力

    public float moveSpeed = 10f; // 移动速度
    public float jumpForce = 5f; // 跳跃力度

    private Rigidbody rb; // 玩家的刚体组件
    private bool isGrounded; // 玩家是否在地面上
    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 获取相机位置作为发射点
        if (cameraController != null)
        {
            shotPoint = cameraController.cameraTransform;
        }
        lastStepTime = -stepSoundInterval; // 初始化为上次播放步行音效时间的前一个时间间隔
        audioManager = AudioManager.instance;

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Update()
    {
        MovePlayer();
        
        // 检查场景中是否存在标记为 "Basketball" 的对象
        if (GameObject.FindWithTag("Basketball") == null)
            {
            // 按住鼠标左键时显示Slider，并进行蓄力
            if (Input.GetMouseButton(0))
            {
                ShowPowerSlider(true);
                IncreasePower();
            }
            // 释放鼠标左键时发射篮球，并隐藏Slider
            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
                ShowPowerSlider(false);
            }
        }

        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {

        audioManager.PlaySFX(audioManager.jump);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded=false;           

    }


    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // 播放步行音效，但只有在不在跳跃状态且有移动输入时才播放
        if (isGrounded && (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(forwardInput) > 0.1f) && Time.time - lastStepTime > stepSoundInterval)
        {
            lastStepTime = Time.time; // 更新上次播放步行音效的时间
            if (Mathf.Abs(horizontalInput) > Mathf.Abs(forwardInput))
            {
                audioManager.PlaySFX(audioManager.walk2); // 左右移动
            }
            else
            {
                audioManager.PlaySFX(audioManager.walk1); // 前后移动
            }
        }

        Vector3 movement = new Vector3(horizontalInput, 0.0f, forwardInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

    }

    void IncreasePower()
    {
        // 更新当前蓄力，确保不超过最大蓄力
        currentPower = Mathf.Min(currentPower + powerIncreaseRate * Time.deltaTime, maxPower);
        // 更新Slider的值
        powerSlider.value = currentPower / maxPower;
    }

    void Shoot()
    {
        if (shotPoint == null)
        {            
            Debug.LogError("Shot point is not assigned!");
            return;
        }

        // 从 Resources 文件夹加载预制体
        GameObject basketballPrefab = Resources.Load<GameObject>("Basketball");
        
        if (basketballPrefab == null)
        {
            Debug.LogError("Failed to load basketball prefab from Resources folder!");
            return;
        }

        // 实例化预制体
        GameObject basketball = Instantiate(basketballPrefab, shotPoint.position, Quaternion.identity);

        Rigidbody basketballRb = basketball.GetComponent<Rigidbody>();
        if (basketballRb == null)
        {
            Debug.LogError("Rigidbody component is missing on the basketball prefab!");
            return;
        }

        // 应用蓄力来给篮球一个初始速度，方向为相机看向的方向
        basketballRb.velocity = shotPoint.forward * currentPower;
        audioManager.PlaySFX(audioManager.shot);

        // 重置蓄力
        currentPower = minPower;
        // 重置Slider的值
        powerSlider.value = 0f;

    }
    
    void ShowPowerSlider(bool show)
    {
        // 设置Slider的可见性
        powerSlider.gameObject.SetActive(show);
    }
}
