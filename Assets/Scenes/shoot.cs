using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Transform handTransform; // �ֱ۵�Transform
    public GameObject basketballPrefab; // �����Prefab
    private GameObject basketballInstance; // �����е�����ʵ��

    void Initialise()
    {
        // ʵ�������򲢽�����Ϊ�ֱ۵��Ӷ���
        // ���ӵĸ߶�ֵ
        float additionalHeight = 0.213f; // ������������Ҫ���ӵĸ߶�ֵ

        // ʵ�������򣬲����ֲ�Transform�Ļ���������һЩ�߶�
        Vector3 spawnPosition = handTransform.position + new Vector3(0, additionalHeight, 0);
        basketballInstance = Instantiate(basketballPrefab, spawnPosition, Quaternion.identity);
        basketballInstance.transform.SetParent(handTransform);


        // �������������
        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // ��������
            rb.isKinematic = true; // ����������ΪKinematic�Ա�������Ӱ��
        }
    }

    // �Ӷ����¼���������������ͷŲ�Ͷ������
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // �ͷ�����

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // ��������
            rb.isKinematic = false; // ȷ�������Rigidbody����Kinematic

            // �����ʼ�ٶȣ�������Ҫ���е����Է���Ԥ�ڵ�������
            Vector3 initialVelocity = new Vector3(0, 10, 5);
            rb.velocity = initialVelocity;
        }
    }
}