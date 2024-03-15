using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class release : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public Transform handTransform; // �ֱ۵�Transform
    public GameObject basketballPrefab; // �����Prefab
    private GameObject basketballInstance; // �����е�����ʵ��

    void Start()
    {
    }

    // �Ӷ����¼���������������ͷŲ�Ͷ������
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // �ͷ�����

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �����ʼ�ٶȣ�������Ҫ���е����Է���Ԥ�ڵ�������
            Vector3 initialVelocity = new Vector3(0, 10, 5);
            rb.velocity = initialVelocity;
            rb.isKinematic = false; // ȷ�������Rigidbody����Kinematic
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
