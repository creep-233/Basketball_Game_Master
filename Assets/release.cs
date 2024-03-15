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
    public Transform handTransform; // 手臂的Transform
    public GameObject basketballPrefab; // 篮球的Prefab
    private GameObject basketballInstance; // 场景中的篮球实例

    void Start()
    {
    }

    // 从动画事件调用这个函数来释放并投掷篮球
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // 释放篮球

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 计算初始速度，这里需要自行调整以符合预期的抛物线
            Vector3 initialVelocity = new Vector3(0, 10, 5);
            rb.velocity = initialVelocity;
            rb.isKinematic = false; // 确保篮球的Rigidbody不是Kinematic
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
