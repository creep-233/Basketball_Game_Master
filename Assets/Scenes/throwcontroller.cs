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
        // ����Ƿ�������Ҽ��������ڶ�����ť��
        if (Input.GetMouseButtonDown(1))
        {
            // ����Ͷ������
            animator.SetTrigger("throw");
        }
    }

}
