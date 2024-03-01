using System;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    private Rigidbody rb;

    private bool hasScored = false; // 标记篮球是否已经进入篮筐

    private bool hasCollided = false; // 标记是否发生碰撞
    int score;
    int ballNum;
    private AudioManager audioManager;

    void Start(){
        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>()); // 忽略篮球和玩家之间的碰撞
        rb = GetComponent<Rigidbody>(); 
        // 初始化 AudioManager
        audioManager = AudioManager.instance;                       
    }
    // 当篮球进入篮筐时调用
    private void OnTriggerEnter(Collider other)
    {
        score = GlobalManager.Instance.Score;

        Vector3 velocity = GetComponent<Rigidbody>().velocity;

        // 检查篮球的速度向下
        if (velocity.y < 0 && !hasScored && other.CompareTag("Hoop"))
        {
            Debug.Log("进球了！");
            hasScored = true; // 标记篮球已经进入篮筐
            audioManager.PlaySFX(audioManager.walk1);

            // 增加得分
            GlobalManager.Instance.IncreaseScore();
        }
    }


    void Update()
    {

        // 如果发生了碰撞，则开始计时
        if (hasCollided)
        {
            if (rb.velocity.magnitude < 2f)
            {
                ballNum = GlobalManager.Instance.BallNum;

                // 减去1
                if(ballNum>0){
                    GlobalManager.Instance.DecreaseBallNum();
                }
                
                Debug.Log(ballNum);

                // 销毁篮球对象
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        audioManager.PlaySFX(audioManager.wallTouch);

        if (collision.gameObject.CompareTag("Ground"))
        {
            // 标记发生了碰撞
            hasCollided = true;
        }
    }

    }