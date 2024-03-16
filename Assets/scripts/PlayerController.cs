using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private CharacterController cc;
    public float moveSpeed;
    public float jumpSpeed;
    private int jumpCount = 0;

    private float horizontalMove, verticalMove;
    private Vector3 dir;

    public float gravity;
    private Vector3 velocity;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGround;

    public int maxJumpCount = 2;

    // 下蹲相关变量
    public float crouchHeight = 0.5f;
    private float originalHeight;
    public bool isCrouching = false;

    // 视野放大相关变量
    public Camera playerCamera; // 玩家摄像机
    public float normalFOV = 85f; // 正常视野
    public float zoomFOV = 30f; // 放大视野
    public float zoomSpeed = 10f; // 视野变化速度

    private void Start()
    {
       // animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        originalHeight = cc.height; // 存储原始高度

        if (!playerCamera)
        {
            playerCamera = Camera.main; // 如果未手动指定摄像机，自动查找主摄像机
        }
        playerCamera.fieldOfView = normalFOV; // 初始化摄像机FOV
    }

    private void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0; // 重置跳跃次数
        }

        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        dir = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(dir * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            velocity.y = jumpSpeed;
            jumpCount++; // 增加跳跃次数
        }

        // 处理下蹲输入
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // 切换下蹲状态
            cc.height = isCrouching ? crouchHeight : originalHeight; // 根据是否下蹲设置高度
        }
        if (Input.GetKeyDown(KeyCode.F)) // 假设空格键用于跳跃
        {
            animator.SetTrigger("ShootTrigger");
        }

        // 视野放大处理
        if (Input.GetKey(KeyCode.Z)) // 按下Z键触发视野放大
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else // 释放Z键恢复正常视野
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, zoomSpeed * Time.deltaTime);
        }

        velocity.y -= gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
     
}
