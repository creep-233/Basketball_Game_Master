using UnityEngine;

public class BasketballScoring : MonoBehaviour
{
    public GameObject topTrigger;       // 拖拽顶部触发器的游戏对象到这里
    public GameObject bottomTrigger;    // 拖拽底部触发器的游戏对象到这里
    public ParticleSystem scoreEffect; // 拖拽一个粒子效果到这个变量上
    public AudioSource scoreSound; // 拖拽一个声音源到这个变量上
    private bool ballEnteredFromTop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (other.gameObject == topTrigger)
            {
                ballEnteredFromTop = true;
            }
            else if (other.gameObject == bottomTrigger && ballEnteredFromTop)
            {
                IncreaseScore();
                ballEnteredFromTop = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && other.gameObject == topTrigger)
        {
            ballEnteredFromTop = false;
        }
    }

    private void IncreaseScore()
    {
    // 显示得分特效
    if (scoreEffect != null)
    {
        scoreEffect.Play();
    }
    
    // 输出分数到控制台
    Debug.Log("Scored!");

    // 播放声音表示得分
    if (scoreSound != null)
    {
        scoreSound.Play();
    }
    // 注意这里的花括号是闭合 IncreaseScore 方法的
    }
}
    