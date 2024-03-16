using UnityEngine;

public class SimpleAnimationControl : MonoBehaviour
{
    public AnimationClip animClip; // 公开的动画片段变量
    public Animation anim;


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        // 检查是否按下了特定按键，比如空格键
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 播放名为"1r"的动画
           anim.Play(animClip.name);
        }
    }
}
