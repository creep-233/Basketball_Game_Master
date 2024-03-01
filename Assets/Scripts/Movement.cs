using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public float stepSoundInterval = 0.5f; // 步行音效播放的间隔时间


    private Rigidbody playerRB;
    private float horizontalInput;
    private float forwardInput;
    private AudioManager audioManager;
    private float lastStepTime; // 上次播放步行音效的时间
    private bool isJumping; // 是否正在跳跃
    private AudioSource walkSound; // 步行音效的 AudioSource 组件
    void Start()
    {
        audioManager = AudioManager.instance;
        playerRB = GetComponent<Rigidbody>();
        walkSound = GetComponent<AudioSource>(); // 获取当前游戏对象上的 AudioSource 组件
        lastStepTime = -stepSoundInterval; // 初始化为上次播放步行音效时间的前一个时间间隔
    }

    void Update()
    {
        // 获取键盘输入
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // 移动 
        Vector3 movement = new Vector3(horizontalInput, 0.0f, forwardInput);
        transform.Translate(movement * speed * Time.deltaTime);

        // 播放步行音效
        if (!isJumping && (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(forwardInput) > 0.1f) && Time.time - lastStepTime > stepSoundInterval)
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

        // 跳跃
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Debug.Log("Press");
            audioManager.PlaySFX(audioManager.jump);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true; // 标记正在跳跃
            // 在跳跃期间暂停步行音效
            StopWalkSound();
        }
    }

    // OnCollisionEnter() 方法中球与其他物体碰撞时播放 audiomanager.wallTouch
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // 跳跃结束后，取消跳跃标记
            // 如果不在跳跃中，恢复步行音效
            if (!isJumping)
            {
                ResumeWalkSound();
            }
        }
    }
    private void StopWalkSound()
    {
        // 停止播放步行音效
        walkSound.Stop();
    }

    private void ResumeWalkSound()
    {
        // 恢复播放步行音效
        walkSound.Play();
    }
}
