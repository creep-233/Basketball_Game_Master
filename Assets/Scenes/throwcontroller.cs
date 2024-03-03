using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwcontroller : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 检查是否按下鼠标右键（即鼠标第二个按钮）
        if (Input.GetMouseButtonDown(1))
        {
            // 播放投掷动画
            animator.SetTrigger("throw");
        }
    }

}
