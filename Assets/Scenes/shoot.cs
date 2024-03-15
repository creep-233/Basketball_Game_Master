using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Transform handTransform; // �ֱ۵�Transform
    public GameObject basketballPrefab; // �����Prefab
    private GameObject basketballInstance; // �����е�����ʵ��

    void Start()
    {
        // ʵ�������򲢽�����Ϊ�ֱ۵��Ӷ���
        basketballInstance = Instantiate(basketballPrefab, handTransform.position, Quaternion.identity);
        basketballInstance.transform.SetParent(handTransform);
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
}