using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasketballShot : MonoBehaviour
{
    public Animator animator;
    public Camera playerCamera; // ���һ������������������
    private Vector3 initialVelocity; // ��ʼ��ʱ����ֵ
    private float initialSpeed = 10f; // ��ʼ�ٶȵĴ�С
    private float speedAdjustment = -0.5f; // ÿ�ι��ֹ���ʱ�ٶȵĵ���ֵ
    public Transform handTransform; // �ֱ۵�Transform
    public GameObject basketballPrefab; // �����Prefab
    private GameObject basketballInstance; // �����е�����ʵ��
    public LineRenderer lineRenderer;
    private bool isHoldingBall = true;
    private bool isAiming = false; // �����������Ƿ�������׼

    void Start()
    {
        lineRenderer.positionCount = 0;
        // ��ʼ��ʱ������ʾ�κ��߶� }
        initialVelocity = playerCamera.transform.forward * initialSpeed;
        
    }
        void Initialise()
    {
        isHoldingBall = true;
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
        ShowTrajectory();
    }
    void Update()
    {
        
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            initialVelocity -= new Vector3(0, 1f, 0);
            initialSpeed = Mathf.Max(1f, initialSpeed); // ȷ���ٶȲ���С��1
        }

        // �����ٶȷ���
        initialVelocity = playerCamera.transform.forward * initialSpeed + playerCamera.transform.up * 5;
        if (Input.GetMouseButtonDown(1)) // ����Ҽ�����
        {
            isAiming = true; // ������׼״̬
            if (isHoldingBall)
            {
                ShowTrajectory(); // ����׼ʱ��ʾ�켣
            }
        }
        if (Input.GetMouseButtonUp(1)) // ����Ҽ��ɿ�
        {
            isAiming = false; // �˳���׼״̬
            lineRenderer.positionCount = 0; // ֹͣ��ʾ�켣
        }

        // ����Ͷ���߼�
        if (isAiming && Input.GetMouseButtonDown(0)) // ������׼״̬�Ұ���������
        {
            if (isHoldingBall)
            {
                ReleaseAndShootBall();
                isHoldingBall = false;
                isAiming = false; // Ͷ�����˳���׼״̬
                lineRenderer.positionCount = 0; // Ͷ������ʾ������
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) // ����ո��������Ծ
        {
            animator.SetTrigger("ShootTrigger");
        }


    }
    

    void ShowTrajectory()
    {
        int segmentCount = 20; // �������ϵ��߶���
        float segmentScale = 0.1f; // ÿ���߶�֮���ʱ����
        Vector3[] segments = new Vector3[segmentCount];

        segments[0] = basketballInstance.transform.position;

        // ��ʼ�ٶȺͷ���
        Vector3 segVelocity = initialVelocity;

        for (int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * segmentScale;
            segments[i] = segments[0] + segVelocity * elapsedTime + 0.5f * Physics.gravity * Mathf.Pow(elapsedTime, 2);
        }

        lineRenderer.positionCount = segmentCount;
        lineRenderer.SetPositions(segments);
    }

    // �Ӷ����¼���������������ͷŲ�Ͷ������
    public void ReleaseAndShootBall()
    {
        basketballInstance.transform.SetParent(null); // �ͷ�����
        ShowTrajectory();

        Rigidbody rb = basketballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // ��������
            rb.isKinematic = false; // ȷ�������Rigidbody����Kinematic

            // �����ʼ�ٶȣ�������Ҫ���е����Է���Ԥ�ڵ�������
            
            rb.velocity = initialVelocity;
        }
    }
}